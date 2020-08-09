using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTreeRoot
{
    private BTreeNode bTreeRoot;

    public BTreeRoot CreateBTree() {
        BTreeConfig bBreeConfig = new BTreeConfig();
        bBreeConfig.stBTreeName = "PlayerTree";
        bBreeConfig.arrBTreeNodeConfig = new BTreeNodeConfig[8];

        int nodeIndex = 0;
        BTreeNodeConfig treeNodeConfig1 = new BTreeNodeConfig();
        treeNodeConfig1.nodeClassName = "BTreeNode_PrioritySelector";
        treeNodeConfig1.stNodeName = "节点1";
        bBreeConfig.arrBTreeNodeConfig[nodeIndex] = treeNodeConfig1;

        nodeIndex++;
        BTreeNodeConfig treeNodeConfig2 = new BTreeNodeConfig();
        treeNodeConfig2.nodeClassName = "BTreeNode_NonePrioritySelector"; 
        treeNodeConfig2.stNodeName = "节点2";
        treeNodeConfig2.parentIndex = 0;
        bBreeConfig.arrBTreeNodeConfig[nodeIndex] = treeNodeConfig2;

        nodeIndex++;
        BTreeNodeConfig treeNodeConfig3 = new BTreeNodeConfig();
        treeNodeConfig3.nodeClassName = "BTreeNode_Sequence";
        treeNodeConfig3.stNodeName = "节点3";
        treeNodeConfig3.parentIndex = 1;
        bBreeConfig.arrBTreeNodeConfig[nodeIndex] = treeNodeConfig3;

        nodeIndex++;
        BTreeNodeConfig treeNodeConfig4 = new BTreeNodeConfig();
        treeNodeConfig4.nodeClassName = "BTreeNode_PlayerEat"; 
        treeNodeConfig4.stNodeName = "eat";
        treeNodeConfig4.parentIndex = 2;
        BTreePreConditionConfig eatPreConditionConfig = new BTreePreConditionConfig();
        eatPreConditionConfig.preConditionClassName = "BTreePreConditionOr";
        eatPreConditionConfig.listChildPreConditon = new List<string>();
        eatPreConditionConfig.listChildPreConditon.Add("BTreePreConditionPlayerIsAtHome");
        eatPreConditionConfig.listChildPreConditon.Add("BTreePreConditionPlayerIsInMall");
        treeNodeConfig4.preCondition = eatPreConditionConfig;
        bBreeConfig.arrBTreeNodeConfig[nodeIndex] = treeNodeConfig4;

        nodeIndex++;
        BTreeNodeConfig treeNodeConfig5 = new BTreeNodeConfig();
        treeNodeConfig5.nodeClassName = "BTreeNode_PlayerInADaze";
        treeNodeConfig5.stNodeName = "InADaze";
        treeNodeConfig5.parentIndex = 2;
        bBreeConfig.arrBTreeNodeConfig[nodeIndex] = treeNodeConfig5;

        nodeIndex++;
        BTreeNodeConfig treeNodeConfig6 = new BTreeNodeConfig();
        treeNodeConfig6.nodeClassName = "BTreeNode_PlayerEntertain";
        treeNodeConfig6.stNodeName = "entertain";
        treeNodeConfig6.parentIndex = 1;
        BTreePreConditionConfig entertainPreConditionConfig = new BTreePreConditionConfig();
        entertainPreConditionConfig.preConditionClassName = "BTreePreConditionOr";
        entertainPreConditionConfig.listChildPreConditon = new List<string>();
        entertainPreConditionConfig.listChildPreConditon.Add("BTreePreConditionPlayerIsAtHome");
        entertainPreConditionConfig.listChildPreConditon.Add("BTreePreConditionPlayerIsInMall");
        treeNodeConfig6.preCondition = entertainPreConditionConfig;
        bBreeConfig.arrBTreeNodeConfig[nodeIndex] = treeNodeConfig6;

        nodeIndex++;
        BTreeNodeConfig treeNodeConfig7 = new BTreeNodeConfig();
        treeNodeConfig7.nodeClassName = "BTreeNode_PlayerWork";
        treeNodeConfig7.stNodeName = "work";
        treeNodeConfig7.parentIndex = 1;
        BTreePreConditionConfig workPreConditionConfig = new BTreePreConditionConfig();
        workPreConditionConfig.preConditionClassName = "BTreePreConditionPlayerIsInCompany";
        treeNodeConfig7.preCondition = workPreConditionConfig;
        bBreeConfig.arrBTreeNodeConfig[nodeIndex] = treeNodeConfig7;

        nodeIndex++;
        BTreeNodeConfig treeNodeConfig8 = new BTreeNodeConfig();
        treeNodeConfig8.nodeClassName = "BTreeNode_PlayerWalk";
        treeNodeConfig8.stNodeName = "walk";
        treeNodeConfig8.parentIndex = 0;
        bBreeConfig.arrBTreeNodeConfig[nodeIndex] = treeNodeConfig8;

        bTreeRoot = BTreeFactory.instance.CreateBTree(bBreeConfig);
        return this;
    }

    public void Tick(BTreeParamData inputData,ref BTreeParamData outputData) {
        if (bTreeRoot.Evaluate(inputData))
        {
            bTreeRoot.Tick(inputData, ref outputData);
        }
        else 
        {
            bTreeRoot.Transition(inputData);
        }
    }
}
