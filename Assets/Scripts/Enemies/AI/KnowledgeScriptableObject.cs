using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAxis;

public enum KnowledgeEnum { none, health, distance, sanity }

[CreateAssetMenu(fileName = "Knowledge", menuName = "ScriptableObjects/Enemy/AI/Knowledge", order = 1)]
public class KnowledgeScriptableObject : ScriptableObject
{
    public string knowledgeName;
    public KnowledgeEnum knowledgeEnum;
    public KnowledgeType knowledgeType;
    public PredefinedValue predefinedValue;
    public bool isPredefinedValue;
}
