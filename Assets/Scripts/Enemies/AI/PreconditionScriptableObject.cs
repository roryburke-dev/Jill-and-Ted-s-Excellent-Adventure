using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PreconditionsEnum { none, isInArea }
[CreateAssetMenu(fileName = "Precondition", menuName = "ScriptableObjects/Enemy/AI/Precondition", order = 1)]
public class PreconditionScriptableObject : ScriptableObject
{
    public string preconditionName;
    public List<PreconditionsEnum> preconditions;
}
