using Kryz.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletBehavior { linear, sine, scatter }

[CreateAssetMenu(fileName = "Bullet", menuName = "ScriptableObjects/Gun'/Bullet", order = 2)]
public class BulletScriptableObject : ScriptableObject
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
