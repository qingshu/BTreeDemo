using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;


public class BTreeNode
{
    protected string stNodeName;
    private BTreePreCondition preCondition;
    protected BTreeNode parent;
    protected int nChildCount;
    protected List<BTreeNode> listChildList;

    protected const int nInvalidChildIndex = -1;

    public BTreeNode() {
        listChildList = new List<BTreeNode>();
    }

    public void SetNodeName(string nodeName) {
        stNodeName = nodeName;
    }

    public void SetParent(BTreeNode _parent) {
        _parent.AddChild(this);
        parent = _parent;
    }

    public void SetPreCondition(BTreePreCondition _preCondition) {
        preCondition = _preCondition;
    }

    public virtual BTreeNode AddChild(BTreeNode bTreeNode) {
        nChildCount++;
        listChildList.Add(bTreeNode);
        return this;
    }

    public bool IsValidChildIndex(int nIndex) {
        if (nIndex <= nInvalidChildIndex || nIndex >= nChildCount) {
            return false;
        }
        return true;
    }

    public bool Evaluate(BTreeParamData bTreeInputData) {
        //Debug.LogError(stNodeName + "Evaluate");
        return (null == preCondition || preCondition.IsPreCondition(bTreeInputData)) && OnEvaluate(bTreeInputData);
    }

    protected virtual bool OnEvaluate(BTreeParamData bTreeInputData) {
        return true;
    }

    public virtual BTreeRunningStatus Tick(BTreeParamData bTreeInputData, ref BTreeParamData bTreeOutputData) {
        return BTreeRunningStatus.Finish;
    }

    public virtual void Transition(BTreeParamData bTreeInputData) { 
    }
}
