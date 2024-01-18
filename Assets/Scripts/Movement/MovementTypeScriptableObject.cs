using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Movement/MovementType", order = 1)]
public class MovementTypeScriptableObject : ScriptableObject
{
    public int id;
    public string movementTypeName;
    public MovementSegmentsScriptableObject[] accelerationSegments, decelerationSegments;
}
