using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTreeNode_PlayerEat : BTreeActionNode
{
    protected override bool OnEvaluate(BTreeParamData bTreeInputData)
    {
        base.OnEvaluate(bTreeInputData);
        BTreePlayerInputData inputData = bTreeInputData as BTreePlayerInputData;
        return inputData.playerData.money >= PlayerConstData.nCostMinMoneyByEat;
    }

    public override BTreeRunningStatus Tick(BTreeParamData bTreeInputData, ref BTreeParamData bTreeOutputData)
    {
        base.Tick(bTreeInputData, ref bTreeOutputData);
        BTreePlayerInputData inputData = bTreeInputData as BTreePlayerInputData;
        BTreePlayerOutputData outPutData = bTreeOutputData as BTreePlayerOutputData;

        PlayerAction eat = new PlayerAction();
        eat.actionType = PlayerActionType.Eat;
        
        PlayerAction costMoney = new PlayerAction();
        costMoney.actionType = PlayerActionType.ChangeMoney;
        costMoney.actionValue = -Random.Range(PlayerConstData.nCostMinMoneyByEat, inputData.playerData.money);

        PlayerAction getEnergy = new PlayerAction();
        getEnergy.actionType = PlayerActionType.ChangeEnergy;
        getEnergy.actionValue = Random.Range(PlayerConstData.nGetMinEnergyByEat, PlayerConstData.nGetMaxEnergyByEat);
        outPutData.listPlayerAction.Add(eat);
        outPutData.listPlayerAction.Add(costMoney);
        outPutData.listPlayerAction.Add(getEnergy);
        return BTreeRunningStatus.Finish;
    }

    public override void Transition(BTreeParamData bTreeInputData)
    {
        base.Transition(bTreeInputData);
    }
}
