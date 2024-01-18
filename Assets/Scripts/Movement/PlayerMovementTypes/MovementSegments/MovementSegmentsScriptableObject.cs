using Kryz.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Movement/MovementSegment", order = 2)]
public class MovementSegmentsScriptableObject : ScriptableObject
{
    public int id;
    public string segmentName;
    public float velocityMinThreshold, velocityMaxThreshold, speed;
    public EasingFunctionEnum easingFunction;
}
