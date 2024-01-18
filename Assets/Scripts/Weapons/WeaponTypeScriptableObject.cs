using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Weapons/Weapon", order = 1)]
public class WeaponTypeScriptableObject : ScriptableObject
{
    [SerializeField]
    private int id;

    public string weaponName;
    public BulletTypeScriptableObject bulletType;
    public int ammoMax;
}
