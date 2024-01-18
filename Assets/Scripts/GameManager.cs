using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public CameraController cameraController;
    public List<LevelScriptableObject> levelScriptableObjects;
    public Level[] levels;
    public Level currentLevel;
    public Stage currentStage;
    public Room currentRoom;
    public Level credits;

    public int levelIndex, stageIndex, roomIndex;
    public bool stageExited;

    public static GameManager instance;

    private float spawnTimeStamp;

    void Awake() 
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        levelIndex = stageIndex = roomIndex = 0;
        SetLevels();
        ChangeLevel(levels[0]);
        stageExited = false;
        spawnTimeStamp = 0.0f;
    }

    public void Update()
    {
        if (currentRoom.spawnQueue != null && currentRoom.spawnQueue.Length > 0) 
        {
            GameObject[] gameObjects = currentRoom.spawnQueue;
            Transform[] points = currentRoom.spawnQueuePoints;
            float[] timers = currentRoom.spawnTimers;
            spawnTimeStamp += Time.deltaTime;
            for (int i = 0; i < gameObjects.Length; i++) 
            {
                if (spawnTimeStamp > timers[i]) 
                {
                    Instantiate(gameObjects[i], points[i].position, Quaternion.identity, currentRoom.center.transform);
                    GameObject[] newGameObjects = new GameObject[gameObjects.Length - 1];
                    for (int j = 0; j < newGameObjects.Length; j++) 
                    {
                        if (j != i) 
                        {
                            newGameObjects[j] = gameObjects[j];
                        }
                    }
                    currentRoom.spawnQueue = newGameObjects;
                }
            }
        }
    }

    private void SetLevels()
    {
        levels = new Level[levelScriptableObjects.Count];
        for (int i = 0; i < levels.Length; i++)
        {
            Level level = gameObject.AddComponent<Level>();
            level.SetValuesFromScriptableObject(levelScriptableObjects[i]);
            levels[i] = level;
        }
    }

    private void ChangeLevel(Level _level)
    {
        currentLevel = _level;
        ChangeStage(currentLevel.stages[0]);
        levelIndex++;
        stageIndex = roomIndex = 0;
    }

    private void ChangeStage(Stage _stage)
    {
        if (stageExited) 
        {
            ClearStage();
            stageExited = false;
        }
        currentStage = _stage;
        BuildStage();
        cameraController.ChangeTarget(currentRoom.centerPoint.transform);
        cameraController.SetPositionToZero(currentRoom.centerPoint.transform);
        stageIndex++;
        roomIndex = 0;
    }

    private void ChangeRoom(Room _room)
    {
        currentRoom = _room;
        if (cameraController)
        {
            cameraController.ChangeTarget(currentRoom.centerPoint.transform);
        }
        roomIndex++;
    }

    private void ClearStage() 
    {
        GameObject[] rooms = GameObject.FindGameObjectsWithTag("Room");
        for (int j = 0; j < rooms.Length; j++) 
        {
            rooms[j].SetActive(false);
            Destroy(rooms[j]);
        }
    }

    private void BuildStage() 
    {
        foreach (Room room in currentStage.rooms) 
        {
            room.BuildRoom();
        }
        currentRoom = currentStage.rooms[0];
    }

    public void ExitRoom(ExitEnum _exitEnum) 
    {

        switch (_exitEnum) 
        {
            case ExitEnum.exitStage:
                ExitStage();
                break;
            case ExitEnum.left:
                RoomTransition();
                break;
            case ExitEnum.right:
                RoomTransition(); 
                break;
            case ExitEnum.top:
                RoomTransition();
                break;
            case ExitEnum.bottom:
                RoomTransition();
                break;
            default:
                break;
        }
    }

    private void ExitStage() 
    {

        if (!stageExited) 
        {
            stageExited = true;
            if (levelIndex >= levels.Length - 1)
            {
                ChangeLevel(credits);
            }
            else if (stageIndex >= currentLevel.stages.Length - 1)
            {
                ChangeLevel(levels[levelIndex]);
            }
            else
            {
                ChangeStage(currentLevel.stages[stageIndex + 1]);
            }
        }
    }

    private void RoomTransition() 
    {
        if (roomIndex < currentStage.rooms.Length - 1)
        {
            ChangeRoom(currentStage.rooms[roomIndex + 1]);
        }
        else if (stageIndex < currentLevel.stages.Length - 1)
        {
            ChangeStage(currentLevel.stages[stageIndex + 1]);
        }
        else if (levelIndex < levels.Length - 1)
        {
            ChangeLevel(levels[levelIndex + 1]);
        }
        else 
        {
            ChangeLevel(credits);
        }
    }
}

public static class Loader
{
    public enum SceneEnum 
    {
        Loading,
        MainMenu,
        Level1, Level2, Level3, Level4, Level5,
        CutScene0, CutScene1, CutScene2, CutScene3, CutScene4, CutScene5, 
        Credits
    }

    public static void Load(SceneEnum _scene) 
    {
        SceneManager.LoadScene(SceneEnum.Loading.ToString());
        SceneManager.LoadScene(_scene.ToString());
    }
}