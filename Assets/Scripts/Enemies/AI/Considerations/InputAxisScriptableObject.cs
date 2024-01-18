using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAxis;

[CreateAssetMenu(fileName = "InputAxis", menuName = "ScriptableObjects/Enemy/AI/InputAxis", order = 5)]
public class InputAxisScriptableObject : ScriptableObject
{
    public Enemy agent;
    public AxisType axisType;
    public Knowledge knowledge;
}
