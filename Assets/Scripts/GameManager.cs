using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    // Self Reference
    public static GameManager instance;

    // Camera
    public CameraController cameraController;

    // Players
    public GameObject player1Prefab, player2Prefab;
    public PlayerScriptableObject player1ScriptableObject, player2ScriptableObject;
    public InputScriptableObject player1InputScriptableObject, player2InputScriptableObject;

    private GameObject player1, player2;
    private PlayerController player1Controller, player2Controller;
    private PlayerInput player1Input, player2Input;

    // Environment
    public List<LevelScriptableObject> levelScriptableObjects;

    private Level[] levels;
    private Level currentLevel;
    private Stage currentStage;
    private Room currentRoom;
    private Level credits;
    private int levelIndex, stageIndex, roomIndex;
    private float spawnTimeStamp;
    private bool stageExited;

    void Awake() 
    {
        // Set reference and make persistant
        instance = this;
        DontDestroyOnLoad(instance.gameObject);
        DontDestroyOnLoad(cameraController.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] objectsInScene = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in objectsInScene)
        {
            if (obj != null && obj.tag != "GameController" && obj.tag != "MainCamera")
            {
                Destroy(obj);
            }
        }

        // Environment Init
        levelIndex = stageIndex = roomIndex = 0;
        SetLevels();
        ChangeLevel(levels[0]);
        stageExited = false;
        spawnTimeStamp = 0.0f;

        // Init player references
        player1 = Instantiate(player1Prefab);
        player2 = Instantiate(player2Prefab);
        player1Controller = player1.GetComponent<PlayerController>();
        player2Controller = player2.GetComponent<PlayerController>();
        player1Controller.Init(player1ScriptableObject);
        player2Controller.Init(player2ScriptableObject);
        player1.GetComponent<Run>().ChangeRunType(currentStage.runType);
        player2.GetComponent<Run>().ChangeRunType(currentStage.runType);
        player1Input = new PlayerInput();
        player2Input = new PlayerInput();
        player1Input.SetInput(player1InputScriptableObject);
        player2Input.SetInput(player2InputScriptableObject);
        player1Controller.input = player1Input;
        player2Controller.input = player2Input;
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
        UpdatePlayers();
    }

    void UpdatePlayers() 
    {
        if (player1Controller.GetIsExitingRoom() || player2Controller.GetIsExitingRoom()) 
        {
            ExitRoom();
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
        if (player1) 
        {
            player1.GetComponent<Run>().ChangeRunType(currentStage.runType);
        }
        if (player2)
        {
            player2.GetComponent<Run>().ChangeRunType(currentStage.runType);
        }
    }

    public void ExitRoom() 
    {
        switch (currentRoom.exitEnum) 
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

public struct PlayerInput
{
    public KeyBoardInput upInput, downInput, leftInput, rightInput, shoot, pause;
    public KeyCode upKeyCode, downKeyCode, leftKeyCode, rightKeyCode, shootKeyCode, pauseKeyCode;
    public bool shootData, pauseData;
    public Vector2 horizontalAxisData, verticalAxisData;
    public InputDevice inputDevice;

    public void SetInput(InputScriptableObject inputScriptableObject)
    {
        inputDevice = inputScriptableObject.inputDevice;
        upInput = inputScriptableObject.up;
        downInput = inputScriptableObject.down;
        leftInput = inputScriptableObject.left;
        rightInput = inputScriptableObject.right;
        shoot = inputScriptableObject.shoot;
        pause = inputScriptableObject.pause;
        upKeyCode = SetInputString(upInput);
        downKeyCode = SetInputString(downInput);
        leftKeyCode = SetInputString(leftInput);
        rightKeyCode = SetInputString(rightInput);
        shootKeyCode = SetInputString(shoot);
        pauseKeyCode = SetInputString(pause);
    }

    public KeyCode SetInputString(KeyBoardInput keyboardInput) 
    {
        switch (keyboardInput)
        {
            case KeyBoardInput.Q:
                return KeyCode.Q;
            case KeyBoardInput.W:
                return KeyCode.W;
            case KeyBoardInput.E:
                return KeyCode.E;
            case KeyBoardInput.R:
                return KeyCode.R;
            case KeyBoardInput.T:
                return KeyCode.T;
            case KeyBoardInput.Y:
                return KeyCode.Y;
            case KeyBoardInput.U:
                return KeyCode.U;
            case KeyBoardInput.I:
                return KeyCode.I;
            case KeyBoardInput.O:
                return KeyCode.O;
            case KeyBoardInput.P:
                return KeyCode.P;
            case KeyBoardInput.A:
                return KeyCode.A;
            case KeyBoardInput.S:
                return KeyCode.S;
            case KeyBoardInput.D:
                return KeyCode.D;
            case KeyBoardInput.F:
                return KeyCode.F;
            case KeyBoardInput.G:
                return KeyCode.G;
            case KeyBoardInput.H:
                return KeyCode.H;
            case KeyBoardInput.J:
                return KeyCode.J;
            case KeyBoardInput.K:
                return KeyCode.K;
            case KeyBoardInput.L:
                return KeyCode.L;
            case KeyBoardInput.Z:
                return KeyCode.Z;
            case KeyBoardInput.X:
                return KeyCode.X;
            case KeyBoardInput.C:
                return KeyCode.C;
            case KeyBoardInput.V:
                return KeyCode.V;
            case KeyBoardInput.B:
                return KeyCode.B;
            case KeyBoardInput.N:
                return KeyCode.N;
            case KeyBoardInput.M:
                return KeyCode.M;
            case KeyBoardInput.backSpace:
                return KeyCode.Backspace;
            case KeyBoardInput.minus:
                return KeyCode.Minus;
            case KeyBoardInput.plus:
                return KeyCode.Plus;
            case KeyBoardInput.one:
                return KeyCode.Alpha1;
            case KeyBoardInput.two:
                return KeyCode.Alpha2;
            case KeyBoardInput.three:
                return KeyCode.Alpha3;
            case KeyBoardInput.four:
                return KeyCode.Alpha4;
            case KeyBoardInput.five:
                return KeyCode.Alpha5;
            case KeyBoardInput.six:
                return KeyCode.Alpha6;
            case KeyBoardInput.seven:
                return KeyCode.Alpha7;
            case KeyBoardInput.eight:
                return KeyCode.Alpha8;
            case KeyBoardInput.nine:
                return KeyCode.Alpha9;
            case KeyBoardInput.zero:
                return KeyCode.Alpha0;
            case KeyBoardInput.backSlash:
                return KeyCode.Backslash;
            case KeyBoardInput.quote:
                return KeyCode.Quote;
            case KeyBoardInput.colon:
                return KeyCode.Colon;
            case KeyBoardInput.rightBracket:
                return KeyCode.RightBracket;
            case KeyBoardInput.leftBracket:
                return KeyCode.LeftBracket;
            case KeyBoardInput.comma:
                return KeyCode.Comma;
            case KeyBoardInput.period:
                return KeyCode.Period;
            case KeyBoardInput.rightControl:
                return KeyCode.RightControl;
            case KeyBoardInput.leftControl:
                return KeyCode.LeftControl;
            case KeyBoardInput.rightShift:
                return KeyCode.RightShift;
            case KeyBoardInput.leftShift:
                return KeyCode.LeftShift;
            case KeyBoardInput.leftAlt:
                return KeyCode.LeftAlt;
            case KeyBoardInput.rightAlt:
                return KeyCode.RightAlt;
            case KeyBoardInput.leftArrow:
                return KeyCode.LeftArrow;
            case KeyBoardInput.upArrow:
                return KeyCode.UpArrow;
            case KeyBoardInput.rightArrow:
                return KeyCode.RightArrow;
            case KeyBoardInput.downArrow:
                return KeyCode.DownArrow;
            case KeyBoardInput.space:
                return KeyCode.Space;
            case KeyBoardInput.enter:
                return KeyCode.Return;
            default:
                return KeyCode.None;
        }
    }

    public KeyCode SetInputString(ControllerInput controllerInput) 
    {
        switch (controllerInput)
        {
            
            default:
                return KeyCode.None;
        }
    }
}