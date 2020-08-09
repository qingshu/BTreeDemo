using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTreeActionNode : BTreeNode
{
    private static string stLastNodeName;
    public override BTreeRunningStatus Tick(BTreeParamData bTreeInputData, ref BTreeParamData bTreeOutputData)
    {
        //if (stLastNodeName == "work" && stLastNodeName == stNodeName) {
        //    int i = 0;
        //    i++;
        //}
        stLastNodeName = stNodeName;
        return base.Tick(bTreeInputData, ref bTreeOutputData);
    }
}
