using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BTreePreConditionConfig {
    public string preConditionClassName;
    public List<string> listChildPreConditon;
}

[Serializable]
public class BTreeNodeConfig
{
    public string stNodeName;
    public string nodeClassName;
    public BTreePreConditionConfig preCondition;
    public int parentIndex = -1;
}

[Serializable]
public class BTreeConfig : MonoBehaviour
{
    public string stBTreeName;
    public BTreeNodeConfig[] arrBTreeNodeConfig;
}
