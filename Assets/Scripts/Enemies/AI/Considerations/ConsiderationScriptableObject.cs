using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAxis;

[CreateAssetMenu(fileName = "Consideration", menuName = "ScriptableObjects/Enemy/AI/Consideration", order = 2)]
public class ConsiderationScriptableObject : ScriptableObject
{
    public KnowledgeScriptableObject knowledgeScriptableObject;
    public AxisType axis;
    public CurveType curveType;
    public float slope, exponent, verticalShift, horizontalShift;
    public List<Precondition> preconditions;
}
