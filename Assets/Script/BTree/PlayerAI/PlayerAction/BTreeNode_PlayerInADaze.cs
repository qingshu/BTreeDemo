using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTreeNode_PlayerInADaze : BTreeActionNode
{
    protected override bool OnEvaluate(BTreeParamData bTreeInputData)
    {
        base.OnEvaluate(bTreeInputData);
        BTreePlayerInputData inputData = bTreeInputData as BTreePlayerInputData;
        return inputData.playerData.energy >= PlayerConstData.nCostEnergyByDaze && Random.Range(0,10) == 0;
    }

    public override BTreeRunningStatus Tick(BTreeParamData bTreeInputData, ref BTreeParamData bTreeOutputData)
    {
        base.Tick(bTreeInputData, ref bTreeOutputData);
        BTreePlayerOutputData outPutData = bTreeOutputData as BTreePlayerOutputData;

        PlayerAction inADaze = new PlayerAction();
        inADaze.actionType = PlayerActionType.InADaze;

        PlayerAction costEnergy = new PlayerAction();
        costEnergy.actionType = PlayerActionType.ChangeEnergy;
        costEnergy.actionValue = -PlayerConstData.nCostEnergyByDaze;
        outPutData.listPlayerAction.Add(inADaze);
        outPutData.listPlayerAction.Add(costEnergy);
        return BTreeRunningStatus.Finish;
    }

    public override void Transition(BTreeParamData bTreeInputData)
    {
        base.Transition(bTreeInputData);
    }
}
