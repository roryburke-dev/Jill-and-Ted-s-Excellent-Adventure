using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAxis;

[CreateAssetMenu(fileName = "Consideration", menuName = "ScriptableObjects/Enemy/AI/Consideration", order = 2)]
public class ConsiderationScriptableObject : ScriptableObject
{
    public string considerationName;
    public KnowledgeScriptableObject knowledgeScriptableObject;
    public List<InputAxisScriptableObject> inputAxes;
    public List<PreconditionScriptableObject> preconditions;
}
