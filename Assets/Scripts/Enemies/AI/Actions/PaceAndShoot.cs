using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAxis;

public class PaceAndShoot : Action
{
    private GameObject endPointA, endPointB, targetPoint;
    private Vector2 position;
    private bool hasReachedEndPoint;
    private float speed;
    private Enemy enemy;
    private float fireRateTimeStamp;

    public void Start()
    {
        GameObject[] endPoints = GameObject.FindGameObjectsWithTag("PatrolPoint");
        targetPoint = endPointA = endPoints[0];
        endPointB = endPoints[1];
        hasReachedEndPoint = false;
        speed = 0.5f;
        position = new Vector2();
        enemy = GetComponent<Enemy>();
        fireRateTimeStamp = enemy.gun.bullet.fireRate;
        fireRateTimeStamp = 0.0f;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PatrolPoint")) 
        {
            hasReachedEndPoint = true;
        }
    }

    public override void ExecuteAction()
    {
        Shoot();
        if (hasReachedEndPoint)
        {
            if (targetPoint == endPointA)
            {
                targetPoint = endPointB;
            }
            else
            {
                targetPoint = endPointA;
            }
            hasReachedEndPoint = false;
        }
        else 
        {
            position = this.transform.position;
            position = Vector2.Lerp(position, targetPoint.transform.position, Time.deltaTime * speed);
            this.transform.position = position;
        }
    }

    
    public override void SetConsideration(Consideration _consideration)
    {
        considerations ??= new List<Consideration>();
        considerations.Add(_consideration);
    }
    
    void Shoot()
    {
        fireRateTimeStamp += Time.deltaTime;
        if (fireRateTimeStamp > enemy.gun.bullet.fireRate)
        {
            Vector2 spawnPosition = enemy.transform.position;
            spawnPosition += Vector2.down * 0.75f;
            Bullet bulletInstance = Instantiate(enemy.bullet, spawnPosition, Quaternion.identity);
            bulletInstance.SetValuesFromScriptableObject(enemy.gun.bullet);
            bulletInstance.SetDirection(FacingDirection.south);
            bulletInstance.damage = enemy.gun.bullet.damage;
            fireRateTimeStamp = 0.0f;
        }
    }
}
