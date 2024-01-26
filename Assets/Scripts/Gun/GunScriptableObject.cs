using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Gun'/Gun", order = 1)]
public class GunScriptableObject : ScriptableObject
{
    [SerializeField]
    private int id;

    public string gunName;
    public BulletScriptableObject bullet;
    public int ammoMax;
}
