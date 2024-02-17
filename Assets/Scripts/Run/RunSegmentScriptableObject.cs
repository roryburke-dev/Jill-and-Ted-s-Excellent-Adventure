using Kryz.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RunSegment", menuName = "ScriptableObjects/Run/RunSegment", order = 2)]
public class RunSegmentScriptableObject : ScriptableObject
{
    public int id;
    public string segmentName;
    public float velocityMinThreshold, velocityMaxThreshold, speed;
    public EasingFunctionEnum easingFunction;
    public bool overrideSliderValues;
}

