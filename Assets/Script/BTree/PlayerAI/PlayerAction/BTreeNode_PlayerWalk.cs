using UnityEngine;

public class BTreeNode_PlayerWalk : BTreeActionNode
{
    private const int nHungry = 50;
    private const int nPoor = 40;

    protected override bool OnEvaluate(BTreeParamData bTreeInputData)
    {
        base.OnEvaluate(bTreeInputData);
        BTreePlayerInputData inputData = bTreeInputData as BTreePlayerInputData;
        if (null == inputData) {
            Debug.LogError("判断走路，输入参数为空！");
            return false;
        }
        Player.PlayerData playerData = inputData.playerData;
        if (null == playerData)
        {
            Debug.LogError("判断走路，玩家数据为空！");
            return false;
        }

        if (playerData.pos == Player.PlayerPos.company)
        {
            //要劳逸结合，不能总在上班
            return true;
        }
        else if (playerData.money < PlayerConstData.nCostMinMoneyByEat)
        {
            //放松过头拉，要上班啦
            return true;
        }
        else if (Random.Range(0, 5) == 0)
        {
            //看心情去哪里
            return true;
        }
        return false;
    }

    public override BTreeRunningStatus Tick(BTreeParamData bTreeInputData, ref BTreeParamData bTreeOutputData)
    {
        base.Tick(bTreeInputData, ref bTreeOutputData);
        BTreePlayerInputData inputData = bTreeInputData as BTreePlayerInputData;
        if (null == inputData)
        {
            Debug.LogError("走路，输入参数为空！");
            return BTreeRunningStatus.Error;
        }
        Player.PlayerData playerData = inputData.playerData;
        if (null == playerData)
        {
            Debug.LogError("走路，玩家数据为空！");
            return BTreeRunningStatus.Error;
        }
        BTreePlayerOutputData outPutData = bTreeOutputData as BTreePlayerOutputData;

        PlayerAction walk = new PlayerAction();
        walk.actionType = PlayerActionType.Walk;
        if (playerData.pos == Player.PlayerPos.company )
        {
            //要劳逸结合，不能总在上班
            walk.actionValue = Random.Range(0, 2) == 0 ? (int)Player.PlayerPos.home : (int)Player.PlayerPos.mall;
        }
        else if (playerData.money < PlayerConstData.nCostMinMoneyByEat)
        {
            //要上班啦
            walk.actionValue = (int)Player.PlayerPos.company;
        }
        else
        {
            //看心情去哪里
            int nextPos = (int)playerData.pos + 1;
            int maxPos = (int)Player.PlayerPos.max;
            walk.actionValue = Random.Range(nextPos, nextPos + maxPos - 1) % maxPos;
        }

        PlayerAction costEnergy = new PlayerAction();
        costEnergy.actionType = PlayerActionType.ChangeEnergy;
        costEnergy.actionValue = -PlayerConstData.nCostEnergyByWalk;
        outPutData.listPlayerAction.Add(walk);
        outPutData.listPlayerAction.Add(costEnergy);
        return BTreeRunningStatus.Finish;
    }

    public override void Transition(BTreeParamData bTreeInputData)
    {
        base.Transition(bTreeInputData);
    }
}
