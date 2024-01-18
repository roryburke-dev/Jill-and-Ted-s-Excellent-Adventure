using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public string id;
    public MovementTypeScriptableObject movementType;
    public CameraAngle cameraAngle;
    public CameraBehavior cameraBehavior;
    public Room[] rooms;

    public void SetStageValuesFromScriptableObject(StageTypeScriptableObject _scriptableObject) 
    {
        id = _scriptableObject.id;
        movementType = _scriptableObject.movementType;
        cameraAngle = _scriptableObject.cameraAngle;
        cameraBehavior = _scriptableObject.cameraBehavior;
        rooms = new Room[_scriptableObject.roomScriptableObjects.Count];
        for (int i = 0; i < rooms.Length; i++) 
        {
            Room room = gameObject.AddComponent<Room>();
            room.SetValuesFromScriptableObject(_scriptableObject.roomScriptableObjects[i]);
            rooms[i] = room;
            
        }
    }
}
