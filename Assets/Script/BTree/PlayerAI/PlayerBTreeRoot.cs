using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBTreeRoot : BTreeRoot
{
    static PlayerBTreeRoot() {
        BTreeFactory.instance.AddTressNodeClass("BTreeNode_PlayerEat", typeof(BTreeNode_PlayerEat));
        BTreeFactory.instance.AddTressNodeClass("BTreeNode_PlayerEntertain", typeof(BTreeNode_PlayerEntertain));
        BTreeFactory.instance.AddTressNodeClass("BTreeNode_PlayerInADaze", typeof(BTreeNode_PlayerInADaze));
        BTreeFactory.instance.AddTressNodeClass("BTreeNode_PlayerWalk", typeof(BTreeNode_PlayerWalk));
        BTreeFactory.instance.AddTressNodeClass("BTreeNode_PlayerWork", typeof(BTreeNode_PlayerWork));

        BTreeFactory.instance.AddTressPreConditionClass("BTreePreConditionPlayerIsAtHome", typeof(BTreePreConditionPlayerIsAtHome));
        BTreeFactory.instance.AddTressPreConditionClass("BTreePreConditionPlayerIsInCompany", typeof(BTreePreConditionPlayerIsInCompany));
        BTreeFactory.instance.AddTressPreConditionClass("BTreePreConditionPlayerIsInMall", typeof(BTreePreConditionPlayerIsInMall));
    }
}
