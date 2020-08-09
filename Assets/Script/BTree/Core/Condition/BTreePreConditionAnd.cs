using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTreePreConditionAnd : BTreePreCondition
{
    public override bool IsPreCondition(BTreeParamData bTreeInputData) {
        if (null == listChildPreCondition) {
            return false;
        }
        for (int i = 0; i < listChildPreCondition.Count; i++) {
            if (!listChildPreCondition[i].IsPreCondition(bTreeInputData)) {
                return false;
            }
        }
        return true;
    }
}
