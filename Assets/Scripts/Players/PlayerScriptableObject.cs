using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "ScriptableObjects/Players/Player", order = 1)]
public class PlayerScriptableObject : ScriptableObject
{
    public float maxHealth, health;
    public GunScriptableObject gun;
}
