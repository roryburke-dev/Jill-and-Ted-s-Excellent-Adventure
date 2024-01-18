using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public int id;
    public Loader.SceneEnum sceneEnum;
    public Stage[] stages;
    public Stage currentStage;

    public void Start()
    {
        currentStage = stages[0];
    }

    public void SetValuesFromScriptableObject(LevelScriptableObject _levelScriptableObject) 
    {
        id = _levelScriptableObject.id;
        sceneEnum = _levelScriptableObject.sceneEnum;
        stages = new Stage[_levelScriptableObject.stages.Count];
        for (int i = 0; i < stages.Length; i++)
        {
            Stage stage = gameObject.AddComponent<Stage>();
            stage.SetStageValuesFromScriptableObject(_levelScriptableObject.stages[i]);
            stages[i] = stage;
        }
    }
}
