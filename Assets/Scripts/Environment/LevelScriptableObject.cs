using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level/Level", order = 0)]
public class LevelScriptableObject : ScriptableObject
{
    public string levelName;
    public Loader.SceneEnum sceneEnum;
    public List<StageScriptableObject> stages;
}
        