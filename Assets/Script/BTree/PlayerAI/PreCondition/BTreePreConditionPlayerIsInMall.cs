using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTreePreConditionPlayerIsInMall : BTreePreCondition
{
    public override bool IsPreCondition(BTreeParamData bTreeInputData)
    {
        BTreePlayerInputData inputData = bTreeInputData as BTreePlayerInputData;
        return inputData.playerData.pos == Player.PlayerPos.mall;
    }
}
