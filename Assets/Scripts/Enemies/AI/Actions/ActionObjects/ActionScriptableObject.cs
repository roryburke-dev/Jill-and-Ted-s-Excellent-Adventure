using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAxis;

[CreateAssetMenu(fileName = "Action", menuName = "ScriptableObjects/Enemy/AI/Action", order = 3)]
public class ActionScriptableObject : ScriptableObject
{
    public int id;
    public List<ConsiderationScriptableObject> considerationsList;
}
