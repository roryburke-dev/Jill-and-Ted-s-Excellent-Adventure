using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BehaviorSet", menuName = "ScriptableObjects/Enemy/AI/BehaviorSet", order = 4)]
public class BehaviorSetScriptableObject : ScriptableObject
{
    public string behaviorSetName;
    public List<ActionScriptableObject> actions;
}
