using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTreePreConditionPlayerIsAtHome : BTreePreCondition
{
    public override bool IsPreCondition(BTreeParamData bTreeInputData)
    {
        BTreePlayerInputData inputData = bTreeInputData as BTreePlayerInputData;
        return inputData.playerData.pos == Player.PlayerPos.home;
    }
}
