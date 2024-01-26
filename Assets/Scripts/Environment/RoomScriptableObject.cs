using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Room", menuName = "ScriptableObjects/Level/Room", order = 2)]
public class RoomScriptableObject : ScriptableObject
{
    public int id;
    public string roomName;
    public GameObject centerPoint;

    [Tooltip("X value for Vector2 of enemy instances")]
    public List<GameObject> enemyPrefabs;

    [Tooltip("X: index in enemyPrefabs list; Y: index in spawnPoints list; Z: spawn time (0 is instant)")]
    public List<Vector3> enemyInstance;

    [Tooltip("X value for Vector2 of environment instances; overloaded prefabs that don't appear in the environment instance list will be instantiated at their prefab specifications.")]
    public List<GameObject> environmentPrefabs;

    [Tooltip("X: index in environmentPrefabs list; Y: index in spawnPoints list; Z: spawn time (0 is instant)")]
    public List<Vector3> environmentInstance;

    [Tooltip("Y value for Vector2 of scene instances")]
    public List<GameObject> spawnPoints;

    public GameObject exitPrefab;
    public ExitEnum exitEnum;
}
