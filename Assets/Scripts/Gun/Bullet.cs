using Kryz.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{  
    public float speed;
    public float damage;
    public float fireRate;

    [HideInInspector]
    public GameObject owner;

    private new Rigidbody2D rigidbody;
    private Vector2 direction;
    public Vector2 velocity;

    private int id;
    private string bulletName;
    private Sprite sprite;
    private EasingFunctionEnum easingFunction;
    private BulletBehavior behavior;

    private bool canDestroy;

    // Update is called once per frame
    void Update()
    {
        velocity = Vector2.Lerp(velocity, direction * speed, EasingFunctions.GetEasingFunctionFromEnum(easingFunction, Time.deltaTime));
        if (canDestroy)
        {
            Destroy(this.gameObject);
        }
    }

    private void FixedUpdate()
    {
        rigidbody.velocity = velocity;
    }

    public void SetValuesFromScriptableObject(BulletScriptableObject bulletType) 
    {
        rigidbody = GetComponent<Rigidbody2D>();
        velocity = Vector2.zero;
        id = bulletType.id;
        bulletName = bulletType.bulletName;
        sprite = bulletType.sprite;
        speed = bulletType.speed;
        damage = bulletType.damage;
        fireRate = bulletType.fireRate;
        easingFunction = bulletType.easingFunction;
        behavior = bulletType.behavior;
        canDestroy = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || !owner.CompareTag("Player"))
        {
            DoDamageToPlayer(collision.gameObject);
        }
        else 
        {
            canDestroy = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            DoDamageToEnemy(collision.gameObject);
        }
        else
        {
            canDestroy = true;
        }
    }

    private void DoDamageToPlayer(GameObject gameObject) 
    {
        PlayerController playerController = gameObject.GetComponent<PlayerController>();
        gameObject.GetComponent<PlayerController>().SetHealth(-damage);
        canDestroy = true;
    }

    public void DoDamageToEnemy(GameObject gameObject) 
    {
        Enemy enemy = gameObject.GetComponent<Enemy>();
        //float enemyHealth = enemy.GetKnowledgeValue(KnowledgeEnum.health);
        //enemyHealth -= damage;
        //enemy.SetKnowledgeValue(KnowledgeEnum.health, enemyHealth);
        canDestroy = true;
    }

    public void SetDirection(FacingDirection directionEnum) 
    {
        switch (behavior) 
        {
            case BulletBehavior.linear: 
                break;
            case BulletBehavior.scatter: 
                break;
            case BulletBehavior.sine: 
                break;
        }
        switch (directionEnum) 
        {
            case FacingDirection.north:
                direction += Vector2.up;
                break;
            case FacingDirection.south:
                direction += Vector2.down;
                break;
            case FacingDirection.east:
                direction += Vector2.right;
                break;
            case FacingDirection.west:
                direction += Vector2.left;
                break;
            case FacingDirection.northEast:
                direction += Vector2.up + Vector2.right * 0.7071f;
                break;
            case FacingDirection.southEast:
                direction += Vector2.down + Vector2.right * 0.7071f;
                break;
            case FacingDirection.northWest:
                direction += Vector2.up + Vector2.left * 0.7071f;
                break;
            case FacingDirection.southWest:
                direction += Vector2.down + Vector2.left * 0.7071f;
                break;
        }
    }
}
