using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConstData {
    public const int nCostMinMoneyByEat = 15;
    public const int nGetMinEnergyByEat = 160;
    public const int nGetMaxEnergyByEat = 320;
    public const int nCostMaxEnergy = 220;


    public const int nCostMinMoneyByEntertain = 30;
    public const int nCostMinEnergyByEntertain = 20;

    public const int nEarnMinMoneyByWork = 260;
    public const int nEarnMaxMoneyByWork = 1200;
    public const int nCostMinEnergyByWork = 80;

    public const int nCostEnergyByDaze = 1;
    public const int nCostEnergyByWalk = 2;
}

public class Player : MonoBehaviour
{
    public enum PlayerPos { 
        home,
        company,
        mall,
        max,
    }

    public class PlayerData {
        public string name;
        public int money;
        public int energy;
        public PlayerPos pos;
    }

    private string[] posName;
    private PlayerBTreeRoot playerBTreeRoot;
    private PlayerData playerData;
    private BTreePlayerInputData playerTreeInputData = new BTreePlayerInputData();
    private BTreePlayerOutputData playerTreeOutputData = new BTreePlayerOutputData();

    public delegate void OnAction(int value);
    private OnAction[] actionList;

    // Start is called before the first frame update
    void Start()
    {
        playerData = new PlayerData();
        playerData.name = "zhangsan";
        playerData.energy = 10;
        playerData.money = 260;
        Debug.LogWarning(string.Format("{0}信息，money:{1},energy:{2}", playerData.name, playerData.money, playerData.energy));

        //创建玩家行为树
        playerBTreeRoot = new PlayerBTreeRoot();
        playerBTreeRoot.CreateBTree();

        posName = new string[(int)PlayerPos.max];
        posName[(int)PlayerPos.company] = "Company";
        posName[(int)PlayerPos.home] = "Home";
        posName[(int)PlayerPos.mall] = "Mall";

        actionList = new OnAction[(int)PlayerActionType.max];
        actionList[(int)PlayerActionType.Eat] = OnEat;
        actionList[(int)PlayerActionType.ChangeMoney] = OnChangeMoney;
        actionList[(int)PlayerActionType.ChangeEnergy] = OnChangeEnergy;
        actionList[(int)PlayerActionType.Entertain] = OnEntertain;
        actionList[(int)PlayerActionType.Walk] = OnWalk;
        actionList[(int)PlayerActionType.InADaze] = OnInADaze;
        actionList[(int)PlayerActionType.work] = OnWork;
    }

    // Update is called once per frame
    void Update()
    {
        playerTreeInputData.playerData = playerData;
        playerTreeOutputData.listPlayerAction.Clear();
        BTreeParamData outputData = playerTreeOutputData as BTreeParamData;
        playerBTreeRoot.Tick(playerTreeInputData, ref outputData);
        playerTreeOutputData = outputData as BTreePlayerOutputData;

        //执行行为
        for (int i = 0; i < playerTreeOutputData.listPlayerAction.Count; i++) {
            PlayerAction action = playerTreeOutputData.listPlayerAction[i];
            if (action.actionType < 0 || action.actionType >= PlayerActionType.max) {
                continue;
            }
            actionList[(int)action.actionType](action.actionValue);
        }
        Debug.LogWarning(string.Format("{0}信息，money:{1},energy:{2},pos:{3}", 
            playerData.name, playerData.money, playerData.energy, posName[(int)playerData.pos]));
    }

    private void OnEat(int value) {
        Debug.LogError(string.Format("{0}吃了一顿饭", playerData.name));
    }

    private void OnChangeMoney(int value)
    {
        if (value > 0)
        {
            Debug.LogError(string.Format("{0}赚了{1}元钱", playerData.name, value));
        }
        else {
            Debug.LogError(string.Format("{0}花了{1}元钱", playerData.name, Math.Abs(value)));
        }
        playerData.money += value;
    }

    private void OnChangeEnergy(int value)
    {
        if (value > 0)
        {
            Debug.LogError(string.Format("{0}获得了{1}能量", playerData.name, value));
        }
        else
        {
            Debug.LogError(string.Format("{0}消耗了{1}能量", playerData.name, Math.Abs(value)));
        }
        playerData.energy += value;
    }

    private void OnEntertain(int value)
    {
        Debug.LogError(string.Format("{0}去娱乐", playerData.name));
    }

    private void OnWalk(int value)
    {
        Debug.LogError(string.Format("{0}走了一段路,从{1}到{2}", playerData.name,posName[(int)playerData.pos],posName[value]));
        playerData.pos = (PlayerPos)value;
    }

    private void OnWork(int value)
    {
        Debug.LogError(string.Format("{0}在工作", playerData.name));
    }

    private void OnInADaze(int value)
    {
        Debug.LogError(string.Format("{0}在发呆", playerData.name));
    }
}
