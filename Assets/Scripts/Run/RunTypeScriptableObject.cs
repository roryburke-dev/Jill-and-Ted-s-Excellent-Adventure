using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Run/RunType", order = 1)]
public class RunTypeScriptableObject : ScriptableObject
{
    public int id;
    public string runTypeName;
    public RunSegmentScriptableObject[] accelerationSegments, decelerationSegments;
}

