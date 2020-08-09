using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// class:      序列节点
/// Evaluate:   若是从头开始的，则调用第一个子节点的Evaluate方法，将其返回值作为自身的返回值返回。否则，调用当前运行节点的Evaluate方法，将其返回值作为自身的返回值返回。
/// Tick:       调用可以运行的子节点的Tick方法，若返回运行结束，则将下一个子节点作为当前运行节点，若当前已是最后一个子节点，表示该序列已经运行结束，则自身返回运行结束。
///             若子节点返回运行中，则用它所返回的运行状态作为自身的运行状态返回
/// </summary>
public class BTreeNode_Sequence : BTreeNode
{
    private int nCurrentChildIndex = -1;
    protected override bool OnEvaluate(BTreeParamData bTreeInputData)
    {
        base.OnEvaluate(bTreeInputData);
        if (!IsValidChildIndex(nCurrentChildIndex)) {
            nCurrentChildIndex = 0;
        }

        return listChildList[nCurrentChildIndex].Evaluate(bTreeInputData);
    }

    public override BTreeRunningStatus Tick(BTreeParamData bTreeInputData, ref BTreeParamData bTreeOutputData)
    {
        base.Tick(bTreeInputData, ref bTreeOutputData);
        if (!IsValidChildIndex(nCurrentChildIndex))
        {
            //Evaluate时都有判断索引，所以这里应该算个异常
            return BTreeRunningStatus.Error;
        }
        BTreeRunningStatus runningStatus = listChildList[nCurrentChildIndex].Tick(bTreeInputData, ref bTreeOutputData);
        if (runningStatus == BTreeRunningStatus.Finish) {
            nCurrentChildIndex++;
            if (nCurrentChildIndex != nChildCount) {
                //所有执行完才算完成
                runningStatus = BTreeRunningStatus.Executing;
            }
        }
        return runningStatus;
    }

    public override void Transition(BTreeParamData bTreeInputData)
    {
        base.Transition(bTreeInputData);
        if (IsValidChildIndex(nCurrentChildIndex)) {
            listChildList[nCurrentChildIndex].Transition(bTreeInputData);
        }
        nCurrentChildIndex = nInvalidChildIndex;
    }
}
