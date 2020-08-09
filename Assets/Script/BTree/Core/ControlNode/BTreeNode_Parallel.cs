using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// class:      并行节点
/// Evaluate:   依次调用所有的子节点的Evaluate方法，若所有的子节点都返回True，则自身也返回True，否则，返回False
/// Tick:       调用所有子节点的Tick方法，若并行节点是“或者”的关系，则只要有一个子节点返回运行结束，那自身就返回运行结束。
///             若并行节点是“并且”的关系，则只有所有的子节点返回结束，自身才返回运行结束
/// </summary>
public class BTreeNode_Parallel : BTreeNode
{
    private List<BTreeRunningStatus> runningStatuses = new List<BTreeRunningStatus>();

    protected override bool OnEvaluate(BTreeParamData bTreeInputData)
    {
        base.OnEvaluate(bTreeInputData);
        for (int i = 0; i < nChildCount; i++) {
            if (runningStatuses[i] == BTreeRunningStatus.Executing && 
                !listChildList[i].Evaluate(bTreeInputData)) {
                return false;
            }
        }
        return true;
    }

    public override BTreeNode AddChild(BTreeNode bTreeNode)
    {
        runningStatuses.Add(BTreeRunningStatus.Executing);
        return base.AddChild(bTreeNode);
    }

    public override BTreeRunningStatus Tick(BTreeParamData bTreeInputData, ref BTreeParamData bTreeOutputData)
    {
        int nFinishCount = 0;
        base.Tick(bTreeInputData,ref bTreeOutputData);
        for (int i = 0; i < nChildCount; i++) {
            if (runningStatuses[i] == BTreeRunningStatus.Executing) {
                runningStatuses[i] = listChildList[i].Tick(bTreeInputData, ref bTreeOutputData);
            }
            if (runningStatuses[i] != BTreeRunningStatus.Executing) {
                nFinishCount++;
            }
        }
        if (nFinishCount == nChildCount) {
            for (int i = 0; i < runningStatuses.Count; i++) {
                runningStatuses[i] = BTreeRunningStatus.Executing;
            }
            return BTreeRunningStatus.Finish;
        }
        return BTreeRunningStatus.Executing;
    }

    public override void Transition(BTreeParamData bTreeInputData)
    {
        base.Transition(bTreeInputData);
        for (int i = 0; i < nChildCount; i++)
        {
            runningStatuses[i] = BTreeRunningStatus.Executing;
            listChildList[i].Transition(bTreeInputData);
        }
    }
}
