using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTreeNode_PlayerWork : BTreeActionNode
{
    protected override bool OnEvaluate(BTreeParamData bTreeInputData)
    {
        base.OnEvaluate(bTreeInputData);
        BTreePlayerInputData inputData = bTreeInputData as BTreePlayerInputData;
        return inputData.playerData.energy >= PlayerConstData.nCostMinEnergyByWork && 
            Random.Range(0, inputData.playerData.money) < PlayerConstData.nCostMinMoneyByEat;
    }

    public override BTreeRunningStatus Tick(BTreeParamData bTreeInputData, ref BTreeParamData bTreeOutputData)
    {
        base.Tick(bTreeInputData, ref bTreeOutputData);
        BTreePlayerInputData inputData = bTreeInputData as BTreePlayerInputData;
        BTreePlayerOutputData outPutData = bTreeOutputData as BTreePlayerOutputData;

        PlayerAction work = new PlayerAction();
        work.actionType = PlayerActionType.work;

        PlayerAction costEnergy = new PlayerAction();
        costEnergy.actionType = PlayerActionType.ChangeEnergy;
        int nMaxEnergy = PlayerConstData.nCostMaxEnergy > inputData.playerData.energy ? inputData.playerData.energy : PlayerConstData.nCostMaxEnergy;
        costEnergy.actionValue = -Random.Range(PlayerConstData.nCostMinEnergyByWork, nMaxEnergy);

        PlayerAction earnMoney = new PlayerAction();
        earnMoney.actionType = PlayerActionType.ChangeMoney;
        earnMoney.actionValue = Random.Range(PlayerConstData.nEarnMinMoneyByWork, PlayerConstData.nEarnMaxMoneyByWork);

        outPutData.listPlayerAction.Add(work);
        outPutData.listPlayerAction.Add(costEnergy);
        outPutData.listPlayerAction.Add(earnMoney);
        return BTreeRunningStatus.Finish;
    }

    public override void Transition(BTreeParamData bTreeInputData)
    {
        base.Transition(bTreeInputData);
    }
}
