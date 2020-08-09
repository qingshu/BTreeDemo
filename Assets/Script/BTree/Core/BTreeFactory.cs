using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BTreeFactory : SingleInstance<BTreeFactory>
{
    private Dictionary<string, Type> dicTreeNodeClass ;
    private Dictionary<string, Type> dicTreePreConditionClass;
    public override void Init()
    {
        base.Init();
        dicTreeNodeClass = new Dictionary<string, Type>();
        dicTreeNodeClass.Add("BTreeNode_Parallel", typeof(BTreeNode_Parallel));
        dicTreeNodeClass.Add("BTreeNode_PrioritySelector", typeof(BTreeNode_PrioritySelector));
        dicTreeNodeClass.Add("BTreeNode_NonePrioritySelector", typeof(BTreeNode_NonePrioritySelector));
        dicTreeNodeClass.Add("BTreeNode_Sequence", typeof(BTreeNode_Sequence));

        dicTreePreConditionClass = new Dictionary<string, Type>();
        dicTreePreConditionClass.Add("BTreePreConditionAnd", typeof(BTreePreConditionAnd));
        dicTreePreConditionClass.Add("BTreePreConditionOr", typeof(BTreePreConditionOr));
        dicTreePreConditionClass.Add("BTreePreConditionNot", typeof(BTreePreConditionNot));
    }

    public override void Dispose()
    {
        base.Dispose();
        dicTreeNodeClass = null;
    }

    public void AddTressNodeClass(string className, Type classType) {
        dicTreeNodeClass[className] = classType;
    }

    public void AddTressPreConditionClass(string className, Type classType)
    {
        dicTreePreConditionClass[className] = classType;
    }

    public BTreeNode CreateBTree(BTreeConfig btreeConfig) {
        BTreeNodeConfig[] listBTreeNodeConfig = btreeConfig.arrBTreeNodeConfig;
        BTreeNode[] bTreeNodeList = new BTreeNode[listBTreeNodeConfig.Length];
        for (int i = 0; i < listBTreeNodeConfig.Length; i++) {
            BTreeNodeConfig nodeConfig = listBTreeNodeConfig[i];
            if (!dicTreeNodeClass.ContainsKey(nodeConfig.nodeClassName)) {
                Debug.LogError(string.Format("创建{0}树节点，不存在该节点类型：{1}", btreeConfig.stBTreeName,nodeConfig.nodeClassName));
                continue;
            }
            BTreeNode btreeNode = Activator.CreateInstance(dicTreeNodeClass[nodeConfig.nodeClassName]) as BTreeNode;
            btreeNode.SetNodeName(nodeConfig.stNodeName);

            //设置父对象
            if (nodeConfig.parentIndex >= 0) {
                BTreeNode bTreeParent = bTreeNodeList[nodeConfig.parentIndex];
                btreeNode.SetParent(bTreeParent);
            }

            //设置外部条件
            if (null != nodeConfig.preCondition) {
                BTreePreConditionConfig preConditionConfig = nodeConfig.preCondition;
                string preConditionClassName = preConditionConfig.preConditionClassName;
                if (dicTreePreConditionClass.ContainsKey(preConditionClassName)) {
                    BTreePreCondition preCondition = Activator.CreateInstance(dicTreePreConditionClass[preConditionClassName]) as BTreePreCondition;

                    //设置子条件
                    if (null != preConditionConfig.listChildPreConditon) {
                        for (int j = 0; j < preConditionConfig.listChildPreConditon.Count; j++)
                        {
                            string childPreConditionClassName = preConditionConfig.listChildPreConditon[j];
                            if (dicTreePreConditionClass.ContainsKey(childPreConditionClassName))
                            {
                                BTreePreCondition childPreCondition = Activator.CreateInstance(dicTreePreConditionClass[childPreConditionClassName]) as BTreePreCondition;
                                preCondition.AddChild(childPreCondition);
                            }
                            else
                            {
                                Debug.LogError(string.Format("创建{0}树节点{1}，不存在该子外部条件类型：{2}",
                                    btreeConfig.stBTreeName, nodeConfig.stNodeName, childPreConditionClassName));
                            }
                        }
                    }
                    btreeNode.SetPreCondition(preCondition);
                }
                else {
                    Debug.LogError(string.Format("创建{0}树节点{1}，不存在该外部条件类型：{2}", 
                        btreeConfig.stBTreeName, nodeConfig.stNodeName,preConditionClassName)); 
                }
                
            }
            bTreeNodeList[i] = btreeNode;
        }
        return bTreeNodeList[0];
    }

    //private void CreateNode(Btr)
}
