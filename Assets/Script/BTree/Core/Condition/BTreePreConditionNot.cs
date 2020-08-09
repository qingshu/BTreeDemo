using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTreePreConditionNot : BTreePreCondition
{
    public override bool IsPreCondition(BTreeParamData bTreeInputData) {
        if (null == listChildPreCondition || listChildPreCondition.Count <= 0) {
            return false;
        }
        return !listChildPreCondition[0].IsPreCondition(bTreeInputData);
    }
}
