using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraAngle { topDown, side }
public enum CameraBehavior { roomToRoom, scrollRight, scrollLeft, scrollUp, scrollDown, still }

[CreateAssetMenu(fileName = "Stage", menuName = "ScriptableObjects/Level/Stage", order = 1)]
public class StageTypeScriptableObject : ScriptableObject
{
    public string id;
    public MovementTypeScriptableObject movementType;
    public CameraAngle cameraAngle;
    public CameraBehavior cameraBehavior;
    public List<RoomScriptableObject> roomScriptableObjects;
}
