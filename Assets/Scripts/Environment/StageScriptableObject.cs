using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraAngle { topDown, side }
public enum CameraBehavior { roomToRoom, scrollRight, scrollLeft, scrollUp, scrollDown, still }

[Serializable]
[CreateAssetMenu(fileName = "Stage", menuName = "ScriptableObjects/Level/Stage", order = 1)]
public class StageScriptableObject : ScriptableObject
{
    public string stageName;
    public RunTypeScriptableObject runType;
    public CameraAngle cameraAngle;
    public CameraBehavior cameraBehavior;
    public List<RoomScriptableObject> roomScriptableObjects;
}
