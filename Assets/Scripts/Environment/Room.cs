using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ExitEnum 
{
    none, exitStage, top, right, left, bottom, scrollTop, scrollLeft, scrollRight, scrollBottom, waitForObjective, waitForTimeLimit
}

public class Room : MonoBehaviour
{
    public int id;
    public GameObject centerPoint;
    public List<GameObject> enemyPrefabs;
    public List<GameObject> environmentPrefabs;
    public List<GameObject> spawnPoints;
    public List<Vector3> enemyInstance;
    public List<Vector3> enivornmentInstance;
    public GameObject exitPrefab;
    public ExitEnum exitEnum;

    public GameObject[] spawnQueue;
    public Transform[] spawnQueuePoints;
    public float[] spawnTimers;
    public GameObject center;

    public void BuildRoom() 
    {
        center = Instantiate(centerPoint);
        center.name = centerPoint.name;
        if (enemyInstance.Count > 0) 
        {
            spawnQueue = new GameObject[enemyInstance.Count];
            spawnQueuePoints = new Transform[enemyInstance.Count];
            spawnTimers = new float[enemyInstance.Count];
            int queueIndex = 0;
            foreach (Vector3 enemyIndex in enemyInstance)
            {
                spawnQueue[queueIndex] = enemyPrefabs[(int)enemyIndex.x];
                spawnQueuePoints[queueIndex] = spawnPoints[(int)enemyIndex.y].transform;
                spawnTimers[queueIndex] = enemyIndex.z;
                queueIndex++;
            }
        }
        
        foreach (GameObject environmentPrefab in environmentPrefabs)
        {
            Instantiate(environmentPrefab, environmentPrefab.transform.position + center.transform.position, environmentPrefab.transform.rotation, center.transform);
        }
        Instantiate(exitPrefab, exitPrefab.transform.position + center.transform.position, exitPrefab.transform.rotation, center.transform);
    }

    public void SetValuesFromScriptableObject(RoomScriptableObject _scriptableObject) 
    {
        id = _scriptableObject.id;
        enemyPrefabs = _scriptableObject.enemyPrefabs;
        spawnPoints = _scriptableObject.spawnPoints;
        enemyInstance = _scriptableObject.enemyInstance;
        environmentPrefabs = _scriptableObject.environmentPrefabs;
        exitPrefab = _scriptableObject.exitPrefab;
        exitEnum = _scriptableObject.exitEnum;
        centerPoint = _scriptableObject.centerPoint;
    }
}
