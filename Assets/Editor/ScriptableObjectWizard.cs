using Kryz.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;


public class ScriptableObjectWizard : EditorWindow
{
#region Scriptable Objects
    LevelScriptableObject levelData;
    StageScriptableObject stageData;
    RoomScriptableObject roomData;
    RunTypeScriptableObject runTypeData;
    RunSegmentScriptableObject runSegmentData;
    GunScriptableObject gunData;
    BulletScriptableObject bulletData;
    EnemyScriptableObject enemyData;
    BehaviorSetScriptableObject behaviorSetData;
    ActionScriptableObject actionData;
    ConsiderationScriptableObject considerationData;
    InputAxisScriptableObject inputAxisData;
    KnowledgeScriptableObject knowledgeData;
    PreconditionScriptableObject preconditionData;
    UpgradeScriptableObject upgradeData;
    InputScriptableObject inputData;
    PlayerScriptableObject playerData;
#endregion

    private enum Panel
    {
        MainMenu, PlayersMenu, RunMenu, GunMenu, EnvironmentMenu, EnemiesMenu, AIMenu, UpgradesMenu, EditOrCreateMenu, PlayersSubMenu,
        Level, Stage, Room, RunType, RunSegment, Enemy, BehaviorSet, Action, Consideration, 
        InputAxis, Knowledge, Precondition, Upgrade, Gun, Bullet, Input,
        CreateLevel, CreateStage, CreateRoom, CreateRunType, CreateRunSegment,
        CreateEnemy, CreateBehaviorSet, CreateAction, CreateConsideration, CreateInputAxis, 
        CreateKnowledge, CreatePrecondition, CreateUpgrade, CreateGun, CreateBullet, CreateInput,
        EditPlayer1, EditPlayer2, EditLevel, EditStage, EditRoom, EditRunType, EditRunSegment, EditEnemy, EditBehaviorSet, 
        EditAction, EditConsideration, EditInputAxis, EditKnowledge, EditPrecondition,
        EditUpgrade, EditGun, EditBullet, EditInput
    }
    private Panel currentPanel;

    [MenuItem("Tools/ScriptableObjectWizard %#z")]
    public static void LaunchWizard()
    {
        ScriptableObjectWizard window = GetWindow<ScriptableObjectWizard>();
        window.titleContent = new GUIContent("Scriptable Object Wizard");
        window.minSize = window.maxSize = new Vector2(333, 420);
    }

    private void OnEnable()
    {
        levelData           = (LevelScriptableObject)ScriptableObject.CreateInstance<LevelScriptableObject>();
        stageData           = (StageScriptableObject)ScriptableObject.CreateInstance<StageScriptableObject>();
        roomData            = (RoomScriptableObject)ScriptableObject.CreateInstance<RoomScriptableObject>();
        runTypeData         = (RunTypeScriptableObject)ScriptableObject.CreateInstance<RunTypeScriptableObject>();
        runSegmentData      = (RunSegmentScriptableObject)ScriptableObject.CreateInstance<RunSegmentScriptableObject>();
        enemyData           = (EnemyScriptableObject)ScriptableObject.CreateInstance<EnemyScriptableObject>();
        behaviorSetData     = (BehaviorSetScriptableObject)ScriptableObject.CreateInstance<BehaviorSetScriptableObject>();
        actionData          = (ActionScriptableObject)ScriptableObject.CreateInstance<ActionScriptableObject>();
        considerationData   = (ConsiderationScriptableObject)ScriptableObject.CreateInstance<ConsiderationScriptableObject>();
        inputAxisData       = (InputAxisScriptableObject)ScriptableObject.CreateInstance<InputAxisScriptableObject>();
        knowledgeData       = (KnowledgeScriptableObject)ScriptableObject.CreateInstance<KnowledgeScriptableObject>();
        preconditionData    = (PreconditionScriptableObject)ScriptableObject.CreateInstance<PreconditionScriptableObject>();
        gunData             = (GunScriptableObject)ScriptableObject.CreateInstance<GunScriptableObject>();
        bulletData          = (BulletScriptableObject)ScriptableObject.CreateInstance<BulletScriptableObject>();
        upgradeData         = (UpgradeScriptableObject)ScriptableObject.CreateInstance<UpgradeScriptableObject>();
        inputData           = (InputScriptableObject)ScriptableObject.CreateInstance<InputScriptableObject>();
        playerData          = (PlayerScriptableObject)ScriptableObject.CreateInstance<PlayerScriptableObject>();
        currentPanel = Panel.MainMenu;
    }

    private Editor editor;
    Vector2 scrollPosition = Vector2.zero;

    private void OnGUI()
    {
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, true);
        editor ??= Editor.CreateEditor(this);
        switch (currentPanel)
        {
            case Panel.MainMenu:
                DrawMainMenuPanel();
                break;
            case Panel.PlayersMenu:
                DrawPlayersMenuPanel();
                break;
            case Panel.EnvironmentMenu:
                DrawEnvironementMenuPanel();
                break;
            case Panel.RunMenu:
                DrawRunMenuPanel();
                break;
            case Panel.EnemiesMenu:
                DrawEnemiesMenuPanel();
                break;
            case Panel.GunMenu:
                DrawGunMenuPanel();
                break;
            case Panel.AIMenu:
                DrawAIMenuPanel();
                break;
            case Panel.PlayersSubMenu:
                DrawPlayerSubPanel();
                break;
            case Panel.Level:
                DrawEditOrNewPanel(Panel.Level);
                break;
            case Panel.Stage:
                DrawEditOrNewPanel(Panel.Stage);
                break;
            case Panel.Room:
                DrawEditOrNewPanel(Panel.Room);
                break;
            case Panel.RunType:
                DrawEditOrNewPanel(Panel.RunType);
                break;
            case Panel.RunSegment:
                DrawEditOrNewPanel(Panel.RunSegment);
                break;
            case Panel.Enemy:
                DrawEditOrNewPanel(Panel.Enemy);
                break;
            case Panel.BehaviorSet:
                DrawEditOrNewPanel(Panel.BehaviorSet);
                break;
            case Panel.Action:
                DrawEditOrNewPanel(Panel.Action);
                break;
            case Panel.Consideration:
                DrawEditOrNewPanel(Panel.Consideration);
                break;
            case Panel.InputAxis:
                DrawEditOrNewPanel(Panel.InputAxis);
                break;
            case Panel.Knowledge:
                DrawEditOrNewPanel(Panel.Knowledge);
                break;
            case Panel.Precondition:
                DrawEditOrNewPanel(Panel.Precondition);
                break;
            case Panel.Upgrade:
                DrawEditOrNewPanel(Panel.Upgrade);
                break;
            case Panel.Gun:
                DrawEditOrNewPanel(Panel.Gun);
                break;
            case Panel.Bullet:
                DrawEditOrNewPanel(Panel.Bullet);
                break;
            case Panel.Input:
                DrawEditOrNewPanel(Panel.Input);
                break;
            case Panel.CreateLevel:
                DrawCreateLevelPanel();
                break;
            case Panel.CreateStage:
                DrawCreateStagePanel();
                break;
            case Panel.CreateRoom:
                DrawCreateRoomPanel();
                break;
            case Panel.CreateRunType:
                DrawCreateRunTypePanel();
                break;
            case Panel.CreateRunSegment:
                DrawCreateRunSegmentPanel();
                break;
            case Panel.CreateEnemy:
                DrawCreateEnemyPanel();
                break;
            case Panel.CreateBehaviorSet:
                DrawCreateBehaviorSetPanel();
                break;
            case Panel.CreateAction:
                DrawCreateActionPanel();
                break;
            case Panel.CreateConsideration:
                DrawCreateConsiderationPanel();
                break;
            case Panel.CreateInputAxis:
                DrawCreateInputAxisPanel();
                break;
            case Panel.CreateKnowledge:
                DrawCreateKnowledgePanel();
                break;
            case Panel.CreatePrecondition:
                DrawCreatePreconditionPanel();
                break;
            case Panel.CreateUpgrade:
                DrawCreateUpgradePanel();
                break;
            case Panel.CreateGun:
                DrawCreateGunPanel();
                break;
            case Panel.CreateBullet:
                DrawCreateBulletPanel();
                break;
            case Panel.CreateInput:
                DrawCreateInputPanel();
                break;
            case Panel.EditPlayer1:
                DrawEditPlayerPanel();
                break;            
            case Panel.EditPlayer2:
                DrawEditPlayerPanel();
                break;
            case Panel.EditLevel:
                DrawEditLevelPanel();
                break;
            case Panel.EditStage:
                DrawEditStagePanel();
                break;
            case Panel.EditRoom:
                DrawEditRoomPanel();
                break;
            case Panel.EditRunType:
                DrawEditRunTypePanel();
                break;
            case Panel.EditRunSegment:
                DrawEditRunSegmentPanel();
                break;
            case Panel.EditEnemy:
                DrawEditEnemyPanel();
                break;
            case Panel.EditBehaviorSet:
                DrawEditBehaviorSetPanel();
                break;
            case Panel.EditAction:
                DrawEditActionPanel();
                break;
            case Panel.EditConsideration:
                DrawEditConsiderationPanel();
                break;
            case Panel.EditInputAxis:
                DrawEditInputAxisPanel();
                break;
            case Panel.EditKnowledge:
                DrawEditKnowledgePanel();
                break;
            case Panel.EditPrecondition:
                DrawEditPreconditionPanel();
                break;
            case Panel.EditUpgrade:
                DrawEditUpgradePanel();
                break;
            case Panel.EditGun:
                DrawEditGunPanel();
                break;
            case Panel.EditBullet:
                DrawEditBulletPanel();
                break;
            case Panel.EditInput:
                DrawEditInputPanel();
                break;
            default: break;
        }
        GUILayout.EndScrollView();
    }

    void OnInspectorUpdate()
    {
        Repaint();
    }

#region Panels

    #region Menus

    void DrawMainMenuPanel()
    {
        if (GUILayout.Button("Run", GUILayout.Height(80)))
        {
            currentPanel = Panel.RunMenu;
        }
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Gun", GUILayout.Height(80)))
        {
            currentPanel = Panel.GunMenu;
        }
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Players", GUILayout.Height(80)))
        {
            currentPanel = Panel.PlayersSubMenu;
        }
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Enemies", GUILayout.Height(80)))
        {
            currentPanel = Panel.EnemiesMenu;
        }
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Environment", GUILayout.Height(80)))
        {
            currentPanel = Panel.EnvironmentMenu;
        }
    }

    void DrawRunMenuPanel()
    {
        if (GUILayout.Button("Run Type", GUILayout.Height(175)))
        {
            currentPanel = Panel.RunType;
        }
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Run Segment", GUILayout.Height(175)))
        {
            currentPanel = Panel.RunSegment;
        }
        AddBackButtons(false);
    }

    void DrawGunMenuPanel()
    {
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Gun", GUILayout.Height(120)))
        {
            currentPanel = Panel.Gun;
        }
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Bullet", GUILayout.Height(120)))
        {
            currentPanel = Panel.Bullet;
        }
        AddBackButtons(false);
    }

    void DrawPlayersMenuPanel() 
    {
        if(GUILayout.Button("Player", GUILayout.Height(120))) 
        {
            currentPanel = Panel.PlayersSubMenu;
        }
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Input", GUILayout.Height(120)))
        {
            currentPanel = Panel.Input;
        }
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Upgrade", GUILayout.Height(120)))
        {
            currentPanel = Panel.Upgrade;
        }
        AddBackButtons(false);
    }

    void DrawEnemiesMenuPanel()
    {
        if (GUILayout.Button("Enemy", GUILayout.Height(175)))
        {
            currentPanel = Panel.Enemy;
        }
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("AI", GUILayout.Height(175)))
        {
            currentPanel = Panel.AIMenu;
        }
        AddBackButtons(false);
    }

    void DrawAIMenuPanel()
    {
        if (GUILayout.Button("Behavior Set", GUILayout.Height(50)))
        {
            currentPanel = Panel.BehaviorSet;
        }
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Action", GUILayout.Height(50)))
        {
            currentPanel = Panel.Action;
        }
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Consideration", GUILayout.Height(50)))
        {
            currentPanel = Panel.Consideration;
        }
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Input Axis", GUILayout.Height(50)))
        {
            currentPanel = Panel.InputAxis;
        }
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Knowledge", GUILayout.Height(50)))
        {
            currentPanel = Panel.Knowledge;
        }
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Precondition", GUILayout.Height(50)))
        {
            currentPanel = Panel.Precondition;
        }
        GUILayout.FlexibleSpace();
        AddBackButtons(false);
    }

    void DrawEnvironementMenuPanel()
    {
        if (GUILayout.Button("Levels", GUILayout.Height(120)))
        {
            currentPanel = Panel.Level;
        }
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Stages", GUILayout.Height(120)))
        {
            currentPanel = Panel.Stage;
        }
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Rooms", GUILayout.Height(120)))
        {
            currentPanel = Panel.Room;
        }
        AddBackButtons(false);
    }

    void DrawPlayerSubPanel() 
    {
        if (GUILayout.Button("Player 1", GUILayout.Height(180))) 
        {
            currentPanel = Panel.EditPlayer1;
        }
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Player 2", GUILayout.Height(180)))
        {
            currentPanel = Panel.EditPlayer2;
        }
        AddBackButtons(false);
    }

    void DrawEditOrNewPanel(Panel nextPanel)
    {
        if (GUILayout.Button("Create " + nextPanel.ToString(), GUILayout.Height(180)))
        {
            switch (nextPanel)
            {
                case Panel.Level:
                    currentPanel = Panel.CreateLevel;
                    break;
                case Panel.Stage:
                    currentPanel = Panel.CreateStage;
                    break;
                case Panel.Room:
                    currentPanel = Panel.CreateRoom;
                    break;
                case Panel.Enemy:
                    currentPanel = Panel.CreateEnemy;
                    break;
                case Panel.BehaviorSet:
                    currentPanel = Panel.CreateBehaviorSet;
                    break;
                case Panel.Action:
                    currentPanel = Panel.CreateAction;
                    break;
                case Panel.Consideration:
                    currentPanel = Panel.CreateConsideration;
                    break;
                case Panel.InputAxis:
                    currentPanel = Panel.CreateInputAxis; 
                    break;
                case Panel.Knowledge:
                    currentPanel = Panel.CreateKnowledge;
                    break;
                case Panel.Precondition:
                    currentPanel = Panel.CreatePrecondition;
                    break;
                case Panel.RunType:
                    currentPanel = Panel.CreateRunType;
                    break;
                case Panel.RunSegment:
                    currentPanel = Panel.CreateRunSegment;
                    break;
                case Panel.Upgrade:
                    currentPanel = Panel.CreateUpgrade;
                    break;
                case Panel.Gun:
                    currentPanel = Panel.CreateGun;
                    break;
                case Panel.Bullet:
                    currentPanel = Panel.CreateBullet;
                    break;
                case Panel.Input: 
                    currentPanel = Panel.CreateInput;
                    break;
                default:
                    break;
            }
        }
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Edit " + nextPanel.ToString(), GUILayout.Height(180)))
        {
            switch (nextPanel)
            {
                case Panel.Level:
                    currentPanel = Panel.EditLevel;
                    break;
                case Panel.Stage:
                    currentPanel = Panel.EditStage;
                    break;
                case Panel.Room:
                    currentPanel = Panel.EditRoom;
                    break;
                case Panel.Enemy:
                    currentPanel = Panel.EditEnemy;
                    break;
                case Panel.BehaviorSet:
                    currentPanel = Panel.EditBehaviorSet;
                    break;
                case Panel.Action:
                    currentPanel = Panel.EditAction;
                    break;
                case Panel.Consideration:
                    currentPanel = Panel.EditConsideration;
                    break;
                case Panel.InputAxis:
                    currentPanel = Panel.EditInputAxis;
                    break;
                case Panel.Knowledge:
                    currentPanel = Panel.EditKnowledge;
                    break;
                case Panel.Precondition:
                    currentPanel = Panel.EditPrecondition;
                    break;
                case Panel.RunType:
                    currentPanel = Panel.EditRunType;
                    break;
                case Panel.RunSegment:
                    currentPanel = Panel.EditRunSegment;
                    break;
                case Panel.Upgrade:
                    currentPanel = Panel.EditUpgrade;
                    break;
                case Panel.Gun:
                    currentPanel = Panel.EditGun;
                    break;
                case Panel.Bullet:
                    currentPanel = Panel.EditBullet;
                    break;
                case Panel.Input: 
                    currentPanel = Panel.EditInput;
                    break;
                default:
                    break;
            }
        }
        AddBackButtons(false);
    }

    void DrawEditOptionsPanel(Panel nextPanel) 
    {
        
    }

    void AddEditButtonOption(Panel nextPanel, string name) 
    {
        
    }

    void DrawEditPanel() 
    {
    
    }

    #endregion

    #region CreatePanels

    [SerializeField] public List<StageScriptableObject> stages = new List<StageScriptableObject>();
    SerializedProperty stagesProperty;
    void DrawCreateLevelPanel()
    {
        stages ??= new List<StageScriptableObject>();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Name:");
        levelData.levelName = EditorGUILayout.TextField(levelData.levelName);
        EditorGUILayout.EndHorizontal();

        // EditorGUILayout.BeginHorizontal();
        // GUILayout.Label("Scene:");
        // levelData.sceneEnum = (Loader.SceneEnum)EditorGUILayout.EnumPopup(levelData.sceneEnum);
        // EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginVertical();
        stagesProperty = editor.serializedObject.FindProperty("stages");
        EditorGUILayout.PropertyField(stagesProperty, new GUIContent("Stages:"), true);
        List<StageScriptableObject> list = new List<StageScriptableObject>();
        foreach (SerializedProperty property in stagesProperty)
        {
            StageScriptableObject stage = (StageScriptableObject)property.boxedValue;
            list.Add(stage);
        }
        levelData.stages = list;
        EditorGUILayout.EndVertical();

        if (GUILayout.Button("Create Level"))
        {
            CreateLevel();
        }
        AddBackButtons(true);
    }

    [SerializeField] public List<RoomScriptableObject> rooms = new List<RoomScriptableObject>();
    [SerializeField] public RunTypeScriptableObject runType;
    SerializedProperty runTypeProperty, roomsProperty;
    void DrawCreateStagePanel()
    {
        rooms ??= new List<RoomScriptableObject>();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Name:");
        stageData.stageName = EditorGUILayout.TextField(stageData.stageName);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        runTypeProperty = editor.serializedObject.FindProperty("runType");
        EditorGUILayout.ObjectField(runTypeProperty, new GUIContent("Run Type:"));
        RunTypeScriptableObject obj = (RunTypeScriptableObject)runTypeProperty.boxedValue;
        stageData.runType = obj;
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Camera Behavior:");
        stageData.cameraBehavior = (CameraBehavior)EditorGUILayout.EnumPopup(stageData.cameraBehavior);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginVertical();
        roomsProperty = editor.serializedObject.FindProperty("rooms");
        EditorGUILayout.PropertyField(roomsProperty, new GUIContent("Rooms:"), true);
        List<RoomScriptableObject> list = new List<RoomScriptableObject>();
        foreach (SerializedProperty property in roomsProperty)
        {
            RoomScriptableObject room = (RoomScriptableObject)property.boxedValue;
            list.Add(room);
        }
        stageData.roomScriptableObjects = list;
        EditorGUILayout.EndVertical();

        if (GUILayout.Button("Create Stage"))
        {
            CreateStage();
        }
        AddBackButtons(true);
    }

    [SerializeField] public GameObject centerPoint;
    [SerializeField] public List<GameObject> enemyPrefabs = new List<GameObject>();
    [SerializeField] public List<Vector3> enemyInstances = new List<Vector3>();
    [SerializeField] public List<GameObject> environmentPrefabs = new List<GameObject>();
    [SerializeField] public List<Vector3> environmentInstances = new List<Vector3>();
    [SerializeField] public List<GameObject> spawnPoints = new List<GameObject>();
    [SerializeField] public GameObject exitPrefab;
    [SerializeField] public GameObject exitEnum;
    SerializedProperty centerPointProperty, enemyPrefabsProperty, enemyInstanceProperty,
        environmentPrefabsProperty, environmentInstancesProperty, spawnPointsProperty,
        exitPrefabProperty, exitEnumProperty;
    void DrawCreateRoomPanel()
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Name:");
        roomData.roomName = EditorGUILayout.TextField(roomData.roomName);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        centerPointProperty = editor.serializedObject.FindProperty("centerPoint");
        EditorGUILayout.ObjectField(centerPointProperty, new GUIContent("Center Point:"));
        GameObject centerObj = (GameObject)centerPointProperty.boxedValue; 
        roomData.centerPoint = centerObj;
        EditorGUILayout.EndHorizontal(); 

        EditorGUILayout.BeginVertical();
        enemyPrefabsProperty = editor.serializedObject.FindProperty("enemyPrefabs");
        EditorGUILayout.PropertyField(enemyPrefabsProperty, new GUIContent("Enemy Prefabs:"), true);
        List<GameObject> enemyPrefabslist = new List<GameObject>();
        foreach (SerializedProperty property in enemyPrefabsProperty)
        {
            GameObject enemyPrefab = (GameObject)property.boxedValue;
            enemyPrefabslist.Add(enemyPrefab);
        }
        roomData.enemyPrefabs = enemyPrefabslist;

        enemyInstanceProperty = editor.serializedObject.FindProperty("enemyInstances");
        EditorGUILayout.PropertyField(enemyInstanceProperty, new GUIContent("Enemy Instances:"), true);
        List<Vector3> enemyInstancesList = new List<Vector3>();
        foreach (SerializedProperty property in enemyInstanceProperty)
        {
            Vector3 enemyInstance = (Vector3)property.boxedValue;
            enemyInstancesList.Add(enemyInstance);
        }
        roomData.enemyInstance = enemyInstancesList;

        environmentPrefabsProperty = editor.serializedObject.FindProperty("environmentPrefabs");
        EditorGUILayout.PropertyField(environmentPrefabsProperty, new GUIContent("Environment Prefabs:"), true);
        List<GameObject> environmentPrefabsList = new List<GameObject>();
        foreach (SerializedProperty property in environmentPrefabsProperty)
        {
            GameObject environmentPrefab = (GameObject)property.boxedValue;
            environmentPrefabsList.Add(environmentPrefab);
        }
        roomData.environmentPrefabs = environmentPrefabsList;

        environmentInstancesProperty = editor.serializedObject.FindProperty("environmentInstances");
        EditorGUILayout.PropertyField(environmentInstancesProperty, new GUIContent("Environment Instances:"), true);
        List<Vector3> environmentInstancesList = new List<Vector3>();
        foreach (SerializedProperty property in environmentInstancesProperty)
        {
            Vector3 environmentInstance = (Vector3)property.boxedValue;
            environmentInstancesList.Add(environmentInstance);
        }
        roomData.environmentInstance = environmentInstancesList;

        spawnPointsProperty = editor.serializedObject.FindProperty("spawnPoints");
        EditorGUILayout.PropertyField(spawnPointsProperty, new GUIContent("Spawn Points:"), true);
        List<GameObject> spawnPointsList = new List<GameObject>();
        foreach (SerializedProperty property in spawnPointsProperty)
        {
            GameObject spawnPoint = (GameObject)property.boxedValue;
            spawnPointsList.Add(spawnPoint);
        }
        roomData.spawnPoints = spawnPointsList;
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginHorizontal();
        exitPrefabProperty = editor.serializedObject.FindProperty("exitPrefab");
        EditorGUILayout.ObjectField(exitPrefabProperty, new GUIContent("Exit Prefab:"));
        GameObject exitObj = (GameObject)exitPrefabProperty.boxedValue;
        roomData.exitPrefab = exitObj;
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Exit Type:");
        roomData.exitEnum = (ExitEnum)EditorGUILayout.EnumPopup(roomData.exitEnum);
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("CreateRoom"))
        {
            CreateRoom();
        }
        AddBackButtons(true);
    }

    [SerializeField] public RunSegmentScriptableObject[] accelerationSegments, decelerationSegments;
    SerializedProperty accelerationSegmentsProperty, decelerationSegmentsProperty;
    void DrawCreateRunTypePanel()
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Name:");
        runTypeData.runTypeName = EditorGUILayout.TextField(runTypeData.runTypeName);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginVertical();
        accelerationSegmentsProperty = editor.serializedObject.FindProperty("accelerationSegments");
        EditorGUILayout.PropertyField(accelerationSegmentsProperty, new GUIContent("Acceleration Segments:"), true);
        List<RunSegmentScriptableObject> accelerationSegmentsList = new List<RunSegmentScriptableObject>();
        foreach (SerializedProperty property in accelerationSegmentsProperty)
        {
            RunSegmentScriptableObject accelerationSegment = (RunSegmentScriptableObject)property.boxedValue;
            accelerationSegmentsList.Add(accelerationSegment);
        }
        accelerationSegments = new RunSegmentScriptableObject[accelerationSegmentsList.Count];
        for (int i = 0; i < accelerationSegments.Length; i++) 
        {
            accelerationSegments[i] = accelerationSegmentsList[i];
        }
        runTypeData.accelerationSegments = accelerationSegments;

        decelerationSegmentsProperty = editor.serializedObject.FindProperty("decelerationSegments");
        EditorGUILayout.PropertyField(decelerationSegmentsProperty, new GUIContent("Deceleration Segments:"), true);
        List<RunSegmentScriptableObject> decelerationSegmentsList = new List<RunSegmentScriptableObject>();
        foreach (SerializedProperty property in decelerationSegmentsProperty)
        {
            RunSegmentScriptableObject decelerationSegment = (RunSegmentScriptableObject)property.boxedValue;
            decelerationSegmentsList.Add(decelerationSegment);
        }
        decelerationSegments = new RunSegmentScriptableObject[decelerationSegmentsList.Count];
        for (int i = 0; i < decelerationSegments.Length; i++)
        {
            decelerationSegments[i] = decelerationSegmentsList[i];
        }
        runTypeData.decelerationSegments = decelerationSegments;
        EditorGUILayout.EndVertical();

        if (GUILayout.Button("Create Run Type"))
        {
            CreateRunType();
        }
        AddBackButtons(true);
    }

    Vector3 velocity;
    void DrawCreateRunSegmentPanel()
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Name:");
        runSegmentData.segmentName = EditorGUILayout.TextField(runSegmentData.segmentName);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Override Slider Values:");
        runSegmentData.overrideSliderValues = EditorGUILayout.Toggle(runSegmentData.overrideSliderValues);
        EditorGUILayout.EndHorizontal();

        if (!runSegmentData.overrideSliderValues)
        {
            EditorGUILayout.BeginHorizontal();
            runSegmentData.velocityMaxThreshold = EditorGUILayout.Slider("Max Velocity", runSegmentData.velocityMaxThreshold, runSegmentData.velocityMinThreshold, 25.0f);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            runSegmentData.velocityMinThreshold = EditorGUILayout.Slider("Min Velocity", runSegmentData.velocityMinThreshold, 0.0f, runSegmentData.velocityMaxThreshold);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            runSegmentData.speed = EditorGUILayout.Slider("Speed", runSegmentData.speed, runSegmentData.velocityMinThreshold, runSegmentData.velocityMaxThreshold);
            EditorGUILayout.EndHorizontal();
        }
        else 
        {
            EditorGUILayout.BeginHorizontal();
            runSegmentData.velocityMaxThreshold = EditorGUILayout.Slider("Max Velocity", runSegmentData.velocityMaxThreshold, 0.0f, 25.0f);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            runSegmentData.velocityMinThreshold = EditorGUILayout.Slider("Min Velocity", runSegmentData.velocityMinThreshold, 0.0f,25.0f);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            runSegmentData.speed = EditorGUILayout.Slider("Speed", runSegmentData.speed, 0.0f, 25.0f);
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Easing Function:");
        runSegmentData.easingFunction = (EasingFunctionEnum)EditorGUILayout.EnumPopup(runSegmentData.easingFunction);
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Create Run Segment"))
        {
            CreateRunSegment();
        }
        AddBackButtons(true);
    }

    [SerializeField] BehaviorSetScriptableObject behaviorSet;
    [SerializeField] List<ActionScriptableObject> extraActions;
    [SerializeField] GameObject loot;
    SerializedProperty behaviorSetProperty, extraActionsProperty, lootProperty;
    void DrawCreateEnemyPanel()
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Name:");
        enemyData.enemyName = EditorGUILayout.TextField(enemyData.enemyName);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        behaviorSetProperty = editor.serializedObject.FindProperty("behaviorSet");
        EditorGUILayout.ObjectField(behaviorSetProperty, new GUIContent("Behavior Set:"));
        BehaviorSetScriptableObject behaviorSetObj = (BehaviorSetScriptableObject)behaviorSetProperty.boxedValue;
        enemyData.behaviorSet = behaviorSetObj;
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginVertical();
        extraActionsProperty = editor.serializedObject.FindProperty("extraActions");
        EditorGUILayout.PropertyField(extraActionsProperty, new GUIContent("Extra Actions:"), true);
        List<ActionScriptableObject> extraActionsList = new List<ActionScriptableObject>();
        foreach (SerializedProperty property in extraActionsProperty)
        {
            ActionScriptableObject extraAction = (ActionScriptableObject)property.boxedValue;
            extraActionsList.Add(extraAction);
        }
        enemyData.extraActions = extraActionsList;
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginHorizontal();
        lootProperty = editor.serializedObject.FindProperty("loot");
        EditorGUILayout.ObjectField(lootProperty, new GUIContent("Loot:"));
        GameObject lootObj = (GameObject)lootProperty.boxedValue;
        enemyData.loot = lootObj;
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Create Enemy"))
        {
            CreateEnemy();
        }
        AddBackButtons(true);
    }

    [SerializeField] List<ActionScriptableObject> actions;
    SerializedProperty actionsProperty;
    void DrawCreateBehaviorSetPanel()
    {
        
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Name:");
        behaviorSetData.behaviorSetName = EditorGUILayout.TextField(behaviorSetData.behaviorSetName);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginVertical();
        actionsProperty = editor.serializedObject.FindProperty("actions");
        EditorGUILayout.PropertyField(actionsProperty, new GUIContent("Actions:"), true);
        List<ActionScriptableObject> actionsList = new List<ActionScriptableObject>();
        foreach (SerializedProperty property in actionsProperty)
        {
            ActionScriptableObject action = (ActionScriptableObject)property.boxedValue;
            actionsList.Add(action);
        }
        behaviorSetData.actions = actionsList;
        EditorGUILayout.EndVertical();
        if (GUILayout.Button("Create Behavior Set"))
        {
            CreateBehaviorSet();
        }
        AddBackButtons(true);
    }
    [SerializeField] List<ConsiderationScriptableObject> considerations;

    SerializedProperty considerationsProperty;
    void DrawCreateActionPanel()
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Name:");
        actionData.actionName = EditorGUILayout.TextField(actionData.actionName);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Action Script:");
        actionData.actionScript = (ActionScript)EditorGUILayout.EnumPopup(actionData.actionScript);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginVertical();
        considerationsProperty = editor.serializedObject.FindProperty("considerations");
        EditorGUILayout.PropertyField(considerationsProperty, new GUIContent("Considerations:"), true);
        List<ConsiderationScriptableObject> considerationsList = new List<ConsiderationScriptableObject>();
        foreach (SerializedProperty property in considerationsProperty)
        {
            ConsiderationScriptableObject consideration = (ConsiderationScriptableObject)property.boxedValue;
            considerationsList.Add(consideration);
        }
        actionData.considerations = considerationsList;
        EditorGUILayout.EndVertical();

        if (GUILayout.Button("Create Action"))
        {
            CreateAction();
        }
        AddBackButtons(true);
    }

    [SerializeField] List<InputAxisScriptableObject> inputAxes;
    [SerializeField] List<PreconditionScriptableObject> preconditions;
    SerializedProperty inputAxesProperty, preconditionsProperty;
    void DrawCreateConsiderationPanel()
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Name:");
        considerationData.considerationName = EditorGUILayout.TextField(considerationData.considerationName);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginVertical();
        inputAxesProperty = editor.serializedObject.FindProperty("inputAxes");
        EditorGUILayout.PropertyField(inputAxesProperty, new GUIContent("Input Axes:"), true);
        List<InputAxisScriptableObject> inputAxesList = new List<InputAxisScriptableObject>();
        foreach (SerializedProperty property in inputAxesProperty)
        {
            InputAxisScriptableObject inputAxis = (InputAxisScriptableObject)property.boxedValue;
            inputAxesList.Add(inputAxis);
        }
        considerationData.inputAxes = inputAxesList;
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical();
        preconditionsProperty = editor.serializedObject.FindProperty("preconditions");
        EditorGUILayout.PropertyField(preconditionsProperty, new GUIContent("Preconditions:"), true);
        List<PreconditionScriptableObject> preconditionsList = new List<PreconditionScriptableObject>();
        foreach (SerializedProperty property in preconditionsProperty)
        {
            PreconditionScriptableObject precondition = (PreconditionScriptableObject)property.boxedValue;
            preconditionsList.Add(precondition);
        }
        considerationData.preconditions = preconditionsList;
        EditorGUILayout.EndVertical();

        if (GUILayout.Button("Create Consideration"))
        {
            CreateConsideration();
        }
        AddBackButtons(true);
    }

    void DrawCreateInputAxisPanel()
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Name:");
        inputAxisData.inputAxisName = EditorGUILayout.TextField(inputAxisData.inputAxisName);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("AxisType:");
        inputAxisData.axis = (UtilityAxis.AxisType)EditorGUILayout.EnumPopup(inputAxisData.axis);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("CurveType:");
        inputAxisData.curveType = (UtilityAxis.CurveType)EditorGUILayout.EnumPopup(inputAxisData.curveType);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Slope:");
        inputAxisData.slope = EditorGUILayout.FloatField(inputAxisData.slope);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Exponent:");
        inputAxisData.exponent = EditorGUILayout.FloatField(inputAxisData.exponent);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Vertical Shift:");
        inputAxisData.verticalShift = EditorGUILayout.FloatField(inputAxisData.verticalShift);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Horizontal Shift:");
        inputAxisData.horizontalShift = EditorGUILayout.FloatField(inputAxisData.horizontalShift);
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Create Input Axis"))
        {
            CreateInputAxis();
        }
        AddBackButtons(true);
    }

    void DrawCreateKnowledgePanel()
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Name:");
        knowledgeData.knowledgeName = EditorGUILayout.TextField(knowledgeData.knowledgeName);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Knowledge Enum:");
        knowledgeData.knowledgeEnum = (KnowledgeEnum)EditorGUILayout.EnumPopup(knowledgeData.knowledgeEnum);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Knowledge Type:");
        knowledgeData.knowledgeType = (UtilityAxis.KnowledgeType)EditorGUILayout.EnumPopup(knowledgeData.knowledgeType);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        knowledgeData.isPredefinedValue = EditorGUILayout.Toggle(knowledgeData.isPredefinedValue);
        if (knowledgeData.isPredefinedValue) 
        {
            GUILayout.Label("Predefined Value:");
            knowledgeData.predefinedValue = (UtilityAxis.PredefinedValue)EditorGUILayout.EnumPopup(knowledgeData.predefinedValue);
        }
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Create Knowledge"))
        {
            CreateKnowledge();
        }
        AddBackButtons(true);
    }

    [SerializeField] List<PreconditionsEnum> preconditionEnums;
    SerializedProperty preconditionEnumsProperty;
    void DrawCreatePreconditionPanel()
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Name:");
        preconditionData.preconditionName = EditorGUILayout.TextField(preconditionData.preconditionName);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginVertical();
        preconditionEnumsProperty = editor.serializedObject.FindProperty("preconditionEnums");
        EditorGUILayout.PropertyField(preconditionEnumsProperty, new GUIContent("Precondition Enums:"), true);
        List<PreconditionsEnum> preconditionsList = new List<PreconditionsEnum>();
        foreach (SerializedProperty property in preconditionEnumsProperty)
        {
            PreconditionsEnum precondition = (PreconditionsEnum)property.boxedValue;
            preconditionsList.Add(precondition);
        }
        preconditionData.preconditions = preconditionsList;
        EditorGUILayout.EndVertical();

        if (GUILayout.Button("Create Precondition"))
        {
            CreatePrecondition();
        }
        AddBackButtons(true);
    }

    void DrawCreateUpgradePanel()
    {
        if (GUILayout.Button("Create Upgrade"))
        {
            CreateUpgrade();
        }
        AddBackButtons(true);
    }

    [SerializeField] BulletScriptableObject bullet;
    SerializedProperty bulletProperty;
    void DrawCreateGunPanel()
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Name:");
        gunData.gunName = EditorGUILayout.TextField(gunData.gunName);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        bulletProperty = editor.serializedObject.FindProperty("bullet");
        EditorGUILayout.ObjectField(bulletProperty, new GUIContent("Bullet:"));
        BulletScriptableObject bulletObj = (BulletScriptableObject)bulletProperty.boxedValue;
        gunData.bullet = bulletObj;
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Ammo Max:");
        gunData.ammoMax = EditorGUILayout.IntField(gunData.ammoMax);
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Create Gun"))
        {
            CreateGun();
        }
        AddBackButtons(true);
    }

    [SerializeField] Sprite sprite;
    SerializedProperty spriteProperty;
    void DrawCreateBulletPanel()
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Name:");
        bulletData.bulletName = EditorGUILayout.TextField(bulletData.bulletName);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        spriteProperty = editor.serializedObject.FindProperty("sprite");
        EditorGUILayout.ObjectField(spriteProperty, new GUIContent("Sprite:"));
        Sprite spriteObj = (Sprite)spriteProperty.boxedValue;
        bulletData.sprite = spriteObj;
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Speed:");
        bulletData.speed = EditorGUILayout.FloatField(bulletData.speed);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Damage:");
        bulletData.damage = EditorGUILayout.FloatField(bulletData.damage);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Fire Rate:");
        bulletData.fireRate = EditorGUILayout.FloatField(bulletData.fireRate);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Easing Function:");
        bulletData.easingFunction = (EasingFunctionEnum)EditorGUILayout.EnumPopup(bulletData.easingFunction);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Behavior:");
        bulletData.behavior = (BulletBehavior)EditorGUILayout.EnumPopup(bulletData.behavior);
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Create Bullet"))
        {
            CreateBullet();
        }
        AddBackButtons(true);
    }

    void DrawCreateInputPanel() 
    {
        if (GUILayout.Button("Create Input")) 
        {
            CreateInput();
        }
        AddBackButtons(true);
    }
    #endregion

    #region EditPanels

    void DrawEditPlayerPanel() 
    {
        if (currentPanel == Panel.EditPlayer1)
        {

        }
        else if(currentPanel == Panel.EditPlayer2)
        {
        
        }
        AddBackButtons(true);
    }

    void DrawEditLevelPanel()
    {
        AddBackButtons(true);
    }

    void DrawEditStagePanel()
    {
        AddBackButtons(true);
    }

    void DrawEditRoomPanel()
    {
        AddBackButtons(true);
    }

    void DrawEditRunTypePanel()
    {
        AddBackButtons(true);
    }

    void DrawEditRunSegmentPanel()
    {
        AddBackButtons(true);
    }

    void DrawEditEnemyPanel()
    {
        AddBackButtons(true);
    }
    void DrawEditBehaviorSetPanel()
    {
        AddBackButtons(true);
    }
    void DrawEditActionPanel()
    {
        AddBackButtons(true);
    }

    void DrawEditConsiderationPanel()
    {
        AddBackButtons(true);
    }

    void DrawEditInputAxisPanel()
    {
        AddBackButtons(true);
    }

    void DrawEditKnowledgePanel()
    {
        AddBackButtons(true);
    }

    void DrawEditPreconditionPanel()
    {
        AddBackButtons(true);
    }

    void DrawEditUpgradePanel()
    {
        AddBackButtons(true);
    }

    void DrawEditGunPanel()
    {
        AddBackButtons(true);
    }

    void DrawEditBulletPanel()
    {
        AddBackButtons(true);
    }

    void DrawEditInputPanel() 
    {
        AddBackButtons(true);
    }

    #endregion

#endregion

#region Create Scriptable Object Functions

    void CreatePlayer() 
    {
    
    }

    void CreateLevel()
    {
        if (levelData.levelName == "") { levelData.levelName = "Level"; }
        string path = AssetDatabase.GenerateUniqueAssetPath("Assets/Resources/ScriptableObjects/Environment/Levels/" + levelData.levelName + ".asset");
        AssetDatabase.CreateAsset(levelData, path);
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = levelData;
        levelData = ScriptableObject.CreateInstance<LevelScriptableObject>();
        stagesProperty.ClearArray();
    }

    void CreateStage() 
    {
        if (stageData.stageName == "") { stageData.stageName = "Stage"; }
        string path = AssetDatabase.GenerateUniqueAssetPath("Assets/Resources/ScriptableObjects/Environment/Stages/" + stageData.stageName + ".asset");
        AssetDatabase.CreateAsset(stageData, path);
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = stageData;
        stageData = ScriptableObject.CreateInstance<StageScriptableObject>();
        roomsProperty.ClearArray();
    }

    void CreateRoom() 
    {
        if (roomData.roomName == "") { roomData.roomName = "Room"; }
        string path = AssetDatabase.GenerateUniqueAssetPath("Assets/Resources/ScriptableObjects/Environment/Rooms/" + roomData.roomName + ".asset");
        AssetDatabase.CreateAsset(roomData, path);
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = roomData;
        roomData = ScriptableObject.CreateInstance<RoomScriptableObject>();
        enemyPrefabsProperty.ClearArray();
        enemyInstanceProperty.ClearArray();
        environmentPrefabsProperty.ClearArray();
        environmentInstancesProperty.ClearArray();
        spawnPointsProperty.ClearArray();
    }

    void CreateRunType() 
    {
        if (runTypeData.runTypeName == "") { runTypeData.runTypeName = "RunType"; }
        string path = AssetDatabase.GenerateUniqueAssetPath("Assets/Resources/ScriptableObjects/Run/RunTypes/" + runTypeData.runTypeName + ".asset");
        AssetDatabase.CreateAsset(runTypeData, path);
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = runTypeData;
        runTypeData = ScriptableObject.CreateInstance<RunTypeScriptableObject>();
        accelerationSegmentsProperty.ClearArray();
        decelerationSegmentsProperty.ClearArray();
    }

    void CreateRunSegment() 
    {
        if (runSegmentData.segmentName == "") { runSegmentData.segmentName = "RunSegment"; }
        string path = AssetDatabase.GenerateUniqueAssetPath("Assets/Resources/ScriptableObjects/Run/RunSegments/" + runSegmentData.segmentName + ".asset");
        AssetDatabase.CreateAsset(runSegmentData, path);
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = runSegmentData;
        runSegmentData = ScriptableObject.CreateInstance<RunSegmentScriptableObject>();
    }

    void CreateEnemy() 
    {
        if (enemyData.enemyName == "") { enemyData.enemyName = "Enemy"; }
        string path = AssetDatabase.GenerateUniqueAssetPath("Assets/Resources/ScriptableObjects/Enemies/Enemy/" + enemyData.enemyName + ".asset");
        AssetDatabase.CreateAsset(enemyData, path);
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = enemyData;
        enemyData = ScriptableObject.CreateInstance<EnemyScriptableObject>();
        extraActionsProperty.ClearArray();
    }

    void CreateBehaviorSet() 
    {
        if (behaviorSetData.behaviorSetName == "") { behaviorSetData.behaviorSetName = "BehaviorSet"; }
        string path = AssetDatabase.GenerateUniqueAssetPath("Assets/Resources/ScriptableObjects/Enemies/AI/BehaviorSets/" + behaviorSetData.behaviorSetName + ".asset");
        AssetDatabase.CreateAsset(behaviorSetData, path);
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = behaviorSetData;
        behaviorSetData = ScriptableObject.CreateInstance<BehaviorSetScriptableObject>();
        actionsProperty.ClearArray();
    }

    void CreateAction() 
    {
        if (actionData.actionName == "") { actionData.actionName = "Action"; }
        string path = AssetDatabase.GenerateUniqueAssetPath("Assets/Resources/ScriptableObjects/Enemies/AI/Actions/" + actionData.actionName + ".asset");
        AssetDatabase.CreateAsset(actionData, path);
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = actionData;
        actionData = ScriptableObject.CreateInstance<ActionScriptableObject>();
        considerationsProperty.ClearArray();
    }

    void CreateConsideration() 
    {
        if (considerationData.considerationName == "") { considerationData.considerationName = "Consideration"; }
        string path = AssetDatabase.GenerateUniqueAssetPath("Assets/Resources/ScriptableObjects/Enemies/AI/Considerations/" + considerationData.considerationName + ".asset");
        AssetDatabase.CreateAsset(considerationData, path);
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = considerationData;
        considerationData = ScriptableObject.CreateInstance<ConsiderationScriptableObject>();
        preconditionsProperty.ClearArray();
    }

    void CreateInputAxis() 
    {
        if (inputAxisData.inputAxisName == "") { inputAxisData.inputAxisName = "InputAxis"; }
        string path = AssetDatabase.GenerateUniqueAssetPath("Assets/Resources/ScriptableObjects/Enemies/AI/InputAxes/" + inputAxisData.inputAxisName + ".asset");
        AssetDatabase.CreateAsset(inputAxisData, path);
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = inputAxisData;
        inputAxisData = ScriptableObject.CreateInstance<InputAxisScriptableObject>();
    }

    void CreateKnowledge() 
    {
        if (knowledgeData.knowledgeName == "") { knowledgeData.knowledgeName = "Knowledge"; }
        string path = AssetDatabase.GenerateUniqueAssetPath("Assets/Resources/ScriptableObjects/Enemies/AI/Knowledges/" + knowledgeData.knowledgeName + ".asset");
        AssetDatabase.CreateAsset(knowledgeData, path);
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = knowledgeData;
        knowledgeData = ScriptableObject.CreateInstance<KnowledgeScriptableObject>();
    }

    void CreatePrecondition() 
    {
        if (preconditionData.preconditionName == "") { preconditionData.preconditionName = "Precondition"; }
        string path = AssetDatabase.GenerateUniqueAssetPath("Assets/Resources/ScriptableObjects/Enemies/AI/Preconditions/" + preconditionData.preconditionName + ".asset");
        AssetDatabase.CreateAsset(preconditionData, path);
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = preconditionData;
        preconditionData = ScriptableObject.CreateInstance<PreconditionScriptableObject>();
        preconditionEnumsProperty.ClearArray();
    }

    void CreateGun() 
    {
        if (gunData.gunName == "") { gunData.gunName = "Gun"; }
        string path = AssetDatabase.GenerateUniqueAssetPath("Assets/Resources/ScriptableObjects/Gun/Guns/" + gunData.gunName + ".asset");
        AssetDatabase.CreateAsset(gunData, path);
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = gunData;
        gunData = ScriptableObject.CreateInstance<GunScriptableObject>();
    }

    void CreateBullet() 
    {
        if (bulletData.bulletName == "") { bulletData.bulletName = "Bullet"; }
        string path = AssetDatabase.GenerateUniqueAssetPath("Assets/Resources/ScriptableObjects/Gun/Bullets/" + bulletData.bulletName + ".asset");
        AssetDatabase.CreateAsset(bulletData, path);
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = bulletData;
        bulletData = ScriptableObject.CreateInstance<BulletScriptableObject>();
    }

    void CreateUpgrade() {}

    void CreateInput() { }

#endregion

#region Utils

    void AddBackButtons(bool doubleBackButton)
    {
        GUILayout.FlexibleSpace();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("<<<"))
        {
            currentPanel = Panel.MainMenu;
        }
        if (doubleBackButton)
        {
            if (GUILayout.Button("<<"))
            {
                switch (currentPanel)
                {
                    case Panel.CreateRunSegment:
                        currentPanel = Panel.RunMenu;
                        break;
                    case Panel.CreateRunType:
                        currentPanel = Panel.RunMenu;
                        break;
                    case Panel.CreateGun:
                        currentPanel = Panel.GunMenu;
                        break;
                    case Panel.CreateBullet:
                        currentPanel = Panel.GunMenu;
                        break;
                    case Panel.CreateInput:
                        currentPanel = Panel.PlayersMenu;
                        break;
                    case Panel.CreateUpgrade:
                        currentPanel = Panel.PlayersMenu;
                        break;
                    case Panel.CreateEnemy:
                        currentPanel = Panel.EnemiesMenu;
                        break;
                    case Panel.CreateBehaviorSet:
                        currentPanel = Panel.AIMenu;
                        break;
                    case Panel.CreateAction:
                        currentPanel = Panel.AIMenu;
                        break;
                    case Panel.CreateConsideration:
                        currentPanel = Panel.AIMenu;
                        break;
                    case Panel.CreateKnowledge:
                        currentPanel = Panel.AIMenu;
                        break;
                    case Panel.CreateInputAxis:
                        currentPanel = Panel.AIMenu;
                        break;
                    case Panel.CreatePrecondition:
                        currentPanel = Panel.AIMenu;
                        break;
                    case Panel.CreateLevel:
                        currentPanel = Panel.EnvironmentMenu;
                        break;
                    case Panel.CreateStage:
                        currentPanel = Panel.EnvironmentMenu;
                        break;
                    case Panel.CreateRoom:
                        currentPanel = Panel.EnvironmentMenu;
                        break;
                    case Panel.EditRunSegment:
                        currentPanel = Panel.RunMenu;
                        break;
                    case Panel.EditRunType:
                        currentPanel = Panel.RunMenu;
                        break;
                    case Panel.EditGun:
                        currentPanel = Panel.GunMenu;
                        break;
                    case Panel.EditBullet:
                        currentPanel = Panel.GunMenu;
                        break;
                    case Panel.EditPlayer1:
                        currentPanel = Panel.PlayersMenu;
                        break;
                    case Panel.EditPlayer2:
                        currentPanel = Panel.PlayersMenu;
                        break;
                    case Panel.EditInput:
                        currentPanel = Panel.PlayersMenu;
                        break;
                    case Panel.EditUpgrade:
                        currentPanel = Panel.PlayersMenu;
                        break;
                    case Panel.EditEnemy:
                        currentPanel = Panel.EnemiesMenu;
                        break;
                    case Panel.EditBehaviorSet:
                        currentPanel = Panel.AIMenu;
                        break;
                    case Panel.EditAction:
                        currentPanel = Panel.AIMenu;
                        break;
                    case Panel.EditConsideration:
                        currentPanel = Panel.AIMenu;
                        break;
                    case Panel.EditKnowledge:
                        currentPanel = Panel.AIMenu;
                        break;
                    case Panel.EditInputAxis:
                        currentPanel = Panel.AIMenu;
                        break;
                    case Panel.EditPrecondition:
                        currentPanel = Panel.AIMenu;
                        break;
                    case Panel.EditLevel:
                        currentPanel = Panel.EnvironmentMenu;
                        break;
                    case Panel.EditStage:
                        currentPanel = Panel.EnvironmentMenu;
                        break;
                    case Panel.EditRoom:
                        currentPanel = Panel.EnvironmentMenu;
                        break;
                    default:
                        currentPanel = Panel.MainMenu;
                        break;
                }
            }
        }
        if (GUILayout.Button("<"))
        {
            switch (currentPanel)
            {
                case Panel.PlayersMenu:
                    currentPanel = Panel.MainMenu;
                    break;
                case Panel.EnvironmentMenu:
                    currentPanel = Panel.MainMenu;
                    break;
                case Panel.RunMenu:
                    currentPanel = Panel.MainMenu;
                    break;
                case Panel.EnemiesMenu:
                    currentPanel = Panel.MainMenu;
                    break;
                case Panel.GunMenu:
                    currentPanel = Panel.MainMenu;
                    break;
                case Panel.AIMenu:
                    currentPanel = Panel.EnemiesMenu;
                    break;
                case Panel.PlayersSubMenu:
                    currentPanel = Panel.PlayersMenu;
                    break;
                case Panel.Level:
                    currentPanel = Panel.EnvironmentMenu;
                    break;
                case Panel.Stage:
                    currentPanel = Panel.EnvironmentMenu;
                    break;
                case Panel.Room:
                    currentPanel = Panel.EnvironmentMenu;
                    break;
                case Panel.RunType:
                    currentPanel = Panel.RunMenu;
                    break;
                case Panel.RunSegment:
                    currentPanel = Panel.RunMenu;
                    break;
                case Panel.Enemy:
                    currentPanel = Panel.EnemiesMenu;
                    break;
                case Panel.BehaviorSet:
                    currentPanel = Panel.AIMenu;
                    break;
                case Panel.Action:
                    currentPanel = Panel.AIMenu;
                    break;
                case Panel.Consideration:
                    currentPanel = Panel.AIMenu;
                    break;
                case Panel.InputAxis:
                    currentPanel = Panel.AIMenu;
                    break;
                case Panel.Knowledge:
                    currentPanel = Panel.AIMenu;
                    break;
                case Panel.Precondition:
                    currentPanel = Panel.AIMenu;
                    break;
                case Panel.Upgrade:
                    currentPanel = Panel.PlayersMenu;
                    break;
                case Panel.Gun:
                    currentPanel = Panel.GunMenu;
                    break;
                case Panel.Bullet:
                    currentPanel = Panel.GunMenu;
                    break;
                case Panel.Input:
                    currentPanel = Panel.PlayersMenu;
                    break;
                case Panel.CreateLevel:
                    currentPanel = Panel.Level;
                    break;
                case Panel.CreateStage:
                    currentPanel = Panel.Stage;
                    break;
                case Panel.CreateRoom:
                    currentPanel = Panel.Room;
                    break;
                case Panel.CreateRunType:
                    currentPanel = Panel.RunType;
                    break;
                case Panel.CreateRunSegment:
                    currentPanel = Panel.RunSegment;
                    break;
                case Panel.CreateEnemy:
                    currentPanel = Panel.Enemy;
                    break;
                case Panel.CreateBehaviorSet:
                    currentPanel = Panel.BehaviorSet;
                    break;
                case Panel.CreateAction:
                    currentPanel = Panel.Action;
                    break;
                case Panel.CreateConsideration:
                    currentPanel = Panel.Consideration;
                    break;
                case Panel.CreateInputAxis:
                    currentPanel = Panel.InputAxis;
                    break;
                case Panel.CreateKnowledge:
                    currentPanel = Panel.Knowledge;
                    break;
                case Panel.CreatePrecondition:
                    currentPanel = Panel.Precondition;
                    break;
                case Panel.CreateUpgrade:
                    currentPanel = Panel.Upgrade;
                    break;
                case Panel.CreateGun:
                    currentPanel = Panel.Gun;
                    break;
                case Panel.CreateBullet:
                    currentPanel = Panel.Bullet;
                    break;
                case Panel.CreateInput:
                    currentPanel = Panel.Input;
                    break;
                case Panel.EditPlayer1:
                    currentPanel = Panel.PlayersSubMenu;
                    break;
                case Panel.EditPlayer2:
                    currentPanel = Panel.PlayersSubMenu;
                    break;
                case Panel.EditLevel:
                    currentPanel = Panel.Level;
                    break;
                case Panel.EditStage:
                    currentPanel = Panel.Stage;
                    break;
                case Panel.EditRoom:
                    currentPanel = Panel.Room;
                    break;
                case Panel.EditRunType:
                    currentPanel = Panel.RunType;
                    break;
                case Panel.EditRunSegment:
                    currentPanel = Panel.RunSegment;
                    break;
                case Panel.EditEnemy:
                    currentPanel = Panel.Enemy;
                    break;
                case Panel.EditBehaviorSet:
                    currentPanel = Panel.BehaviorSet;
                    break;
                case Panel.EditAction:
                    currentPanel = Panel.Action;
                    break;
                case Panel.EditConsideration:
                    currentPanel = Panel.Consideration;
                    break;
                case Panel.EditInputAxis:
                    currentPanel = Panel.InputAxis;
                    break;
                case Panel.EditKnowledge:
                    currentPanel = Panel.Knowledge;
                    break;
                case Panel.EditPrecondition:
                    currentPanel = Panel.Precondition;
                    break;
                case Panel.EditUpgrade:
                    currentPanel = Panel.Upgrade;
                    break;
                case Panel.EditGun:
                    currentPanel = Panel.Gun;
                    break;
                case Panel.EditBullet:
                    currentPanel = Panel.Bullet;
                    break;
                case Panel.EditInput:
                    currentPanel = Panel.Input;
                    break;
                default: break;
            }
        }
        GUILayout.EndHorizontal();
    }

    #endregion

}
