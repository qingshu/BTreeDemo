using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTreePreCondition
{
    protected List<BTreePreCondition> listChildPreCondition;

    public void AddChild(BTreePreCondition childPreCondition) {
        if (null == listChildPreCondition) {
            listChildPreCondition = new List<BTreePreCondition>();
        }
        listChildPreCondition.Add(childPreCondition);
    }

    public virtual bool IsPreCondition(BTreeParamData bTreeInputData) {
        return true;
    }
}
