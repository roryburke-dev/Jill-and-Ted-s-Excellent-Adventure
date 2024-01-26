using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAxis;

[CreateAssetMenu(fileName = "InputAxis", menuName = "ScriptableObjects/Enemy/AI/InputAxis", order = 5)]
public class InputAxisScriptableObject : ScriptableObject
{
    public string inputAxisName;
    public AxisType axis;
    public CurveType curveType;
    public float slope, exponent, verticalShift, horizontalShift;
}
