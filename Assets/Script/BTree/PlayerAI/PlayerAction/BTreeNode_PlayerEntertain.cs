using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTreeNode_PlayerEntertain : BTreeActionNode
{
    protected override bool OnEvaluate(BTreeParamData bTreeInputData)
    {
        base.OnEvaluate(bTreeInputData);
        BTreePlayerInputData inputData = bTreeInputData as BTreePlayerInputData;
        return inputData.playerData.money >= (PlayerConstData.nCostMinMoneyByEntertain + PlayerConstData.nCostMinMoneyByEat) &&
            inputData.playerData.energy >= PlayerConstData.nCostMinEnergyByEntertain;
    }

    public override BTreeRunningStatus Tick(BTreeParamData bTreeInputData, ref BTreeParamData bTreeOutputData)
    {
        base.Tick(bTreeInputData, ref bTreeOutputData);
        BTreePlayerInputData inputData = bTreeInputData as BTreePlayerInputData;
        BTreePlayerOutputData outPutData = bTreeOutputData as BTreePlayerOutputData;

        PlayerAction enterTain = new PlayerAction();
        enterTain.actionType = PlayerActionType.Entertain;

        PlayerAction costMoney = new PlayerAction();
        costMoney.actionType = PlayerActionType.ChangeMoney;
        costMoney.actionValue = -Random.Range(PlayerConstData.nCostMinMoneyByEntertain,inputData.playerData.money - PlayerConstData.nCostMinMoneyByEat);

        PlayerAction costEnergy = new PlayerAction();
        costEnergy.actionType = PlayerActionType.ChangeEnergy;
        int nMaxEnergy = PlayerConstData.nCostMaxEnergy > inputData.playerData.energy ? inputData.playerData.energy : PlayerConstData.nCostMaxEnergy;
        costEnergy.actionValue = -Random.Range(PlayerConstData.nCostMinEnergyByEntertain, nMaxEnergy);
        outPutData.listPlayerAction.Add(enterTain);
        outPutData.listPlayerAction.Add(costMoney);
        outPutData.listPlayerAction.Add(costEnergy);
        return BTreeRunningStatus.Finish;
    }

    public override void Transition(BTreeParamData bTreeInputData)
    {
        base.Transition(bTreeInputData);
    }
}
