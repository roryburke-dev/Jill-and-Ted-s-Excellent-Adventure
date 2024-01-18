using Kryz.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletBehavior { linear, sine, scatter }

[CreateAssetMenu(fileName = "Bullet", menuName = "ScriptableObjects/Weapons/Bullet", order = 2)]
public class BulletTypeScriptableObject : ScriptableObject
{
    public int id;
    public string bulletName;
    public Sprite sprite;
    public float speed;
    public float damage;
    public float fireRate;
    public EasingFunctionEnum easingFunction;
    public BulletBehavior behavior;
}
