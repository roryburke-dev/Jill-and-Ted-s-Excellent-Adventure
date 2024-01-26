using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Enemy/Enemy", order = 1)]
public class EnemyScriptableObject : ScriptableObject
{
    public int id;
    public string enemyName;
    public BehaviorSetScriptableObject behaviorSet;
    public List<ActionScriptableObject> extraActions;
    public GameObject loot;
}
