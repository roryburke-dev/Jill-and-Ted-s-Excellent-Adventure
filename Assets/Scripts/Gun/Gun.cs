using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kryz.Tweening;

public class Gun : MonoBehaviour
{
    public Bullet bullet;

    private PlayerController player;
    private float fireRateTimeStamp;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerController>();
        fireRateTimeStamp = player.gunType.bullet.fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    void Shoot() 
    {
        fireRateTimeStamp += Time.deltaTime;
        if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.Space))
        {
            if (fireRateTimeStamp > player.gunType.bullet.fireRate)
            {
                Vector2 spawnPosition = player.spawnPoint.position;
                switch (player.facingDirection)
                {
                    case FacingDirection.north:
                        spawnPosition += Vector2.up * 0.1f;
                        break;
                    case FacingDirection.south:
                        spawnPosition += Vector2.down * 0.1f;
                        break;
                    case FacingDirection.east:
                        spawnPosition += Vector2.right * 0.1f;
                        break;
                    case FacingDirection.west:
                        spawnPosition += Vector2.left * 0.1f;
                        break;
                    case FacingDirection.northEast:
                        spawnPosition += Vector2.up * 0.1f;
                        spawnPosition += Vector2.right * 0.1f;
                        break;
                    case FacingDirection.southEast:
                        spawnPosition += Vector2.down * 0.1f;
                        spawnPosition += Vector2.right * 0.1f;
                        break;
                    case FacingDirection.northWest:
                        spawnPosition += Vector2.left * 0.1f;
                        spawnPosition += Vector2.up * 0.1f;
                        break;
                    case FacingDirection.southWest:
                        spawnPosition += Vector2.down * 0.1f;
                        spawnPosition += Vector2.left * 0.1f;
                        break;
                }
                Bullet bulletInstance = Instantiate(bullet, spawnPosition, Quaternion.identity);
                bulletInstance.SetValuesFromScriptableObject(player.gunType.bullet);
                bulletInstance.velocity += this.gameObject.GetComponent<Run>().velocity;
                bulletInstance.SetDirection(player.facingDirection);
                bulletInstance.damage = player.damage;
                fireRateTimeStamp = 0.0f;
            }
        }
    }
}
