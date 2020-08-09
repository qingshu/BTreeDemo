using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// class:      带优先级的选择节点
/// Evaluate:   从第一个子节点开始依次遍历所有的子节点，调用其Evaluate方法，当发现存在可以运行的子节点时，记录子节点索引，停止遍历，返回True。
/// Tick:       调用可以运行的子节点的Tick方法，用它所返回的运行状态作为自身的运行状态返回
/// </summary>
public class BTreeNode_PrioritySelector : BTreeNode
{
    protected int nCurrentIndex = nInvalidChildIndex;
    protected int nLstIndex = nInvalidChildIndex;

    protected override bool OnEvaluate(BTreeParamData bTreeInputData)
    {
        base.OnEvaluate(bTreeInputData);
        for (int i = 0; i < nChildCount; i++) {
            if (listChildList[i].Evaluate(bTreeInputData)) {
                nCurrentIndex = i;
                return true;
            }
        }
        return false;
    }

    public override BTreeRunningStatus Tick(BTreeParamData bTreeInputData, ref BTreeParamData bTreeOutputData)
    {
        base.Tick(bTreeInputData, ref bTreeOutputData);
        if (!IsValidChildIndex(nCurrentIndex)) {
            return BTreeRunningStatus.Error;
        }
        if(nCurrentIndex != nLstIndex)
        {
            if (IsValidChildIndex(nLstIndex)) {
                listChildList[nLstIndex].Transition(bTreeInputData);
            }
            nLstIndex = nCurrentIndex;
        }

        return listChildList[nLstIndex].Tick(bTreeInputData, ref bTreeOutputData);
    }

    public override void Transition(BTreeParamData bTreeInputData)
    {
        base.Transition(bTreeInputData);
        if (!IsValidChildIndex(nCurrentIndex)) {
            return;
        }
        listChildList[nCurrentIndex].Transition(bTreeInputData);
    }
}
