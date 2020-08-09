using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerActionType { 
    Walk,
    Eat,
    ChangeMoney,
    ChangeEnergy,
    Entertain,
    work,
    InADaze,
    max,
}

public class PlayerAction {
    public PlayerActionType actionType;
    public int actionValue;
}

public class BTreePlayerOutputData : BTreeParamData
{
    public List<PlayerAction> listPlayerAction = new List<PlayerAction>();
}
