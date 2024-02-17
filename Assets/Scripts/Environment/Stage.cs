using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public string stageName;
    public RunTypeScriptableObject runType;
    public CameraAngle cameraAngle;
    public CameraBehavior cameraBehavior;
    public Room[] rooms;

    public void SetStageValuesFromScriptableObject(StageScriptableObject _scriptableObject) 
    {
        stageName = _scriptableObject.stageName;
        runType = _scriptableObject.runType;
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
