using StatesEnum;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public PlayerInput input;
    public FacingDirection facingDirection;
    public Vector2 bulletSpawnPoint;

    // Exposed for editing and prototype purposes, will be a variable amount attributed from enemy
    public float knockbackAmount;
    
    private new Rigidbody2D rigidbody;
    private GunScriptableObject gun;
    private DamageType damageType;
    private float damageMultiplier;
    private float health, maxHealth;

    private bool isExitingRoom;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Init(PlayerScriptableObject playerScriptableObject) 
    {
        isExitingRoom = false;
        health = maxHealth = playerScriptableObject.health;
        gun = playerScriptableObject.gun;
        damageType = DamageType.normal;
        damageMultiplier = 1;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            health -= 2.0f;

            // Get Knocked back
            switch (facingDirection)
            {
                case FacingDirection.north:
                    rigidbody.AddForce(knockbackAmount * Vector2.down);
                    break;
                case FacingDirection.south:
                    rigidbody.AddForce(knockbackAmount * Vector2.up);
                    break;
                case FacingDirection.east:
                    rigidbody.AddForce(knockbackAmount * Vector2.left);
                    break;
                case FacingDirection.west:
                    rigidbody.AddForce(knockbackAmount * Vector2.right);
                    break;
                case FacingDirection.northEast:
                    rigidbody.AddForce(knockbackAmount * (Vector2.down + Vector2.left));
                    break;
                case FacingDirection.northWest:
                    rigidbody.AddForce(knockbackAmount * (Vector2.down + Vector2.right));
                    break;
                case FacingDirection.southEast:
                    rigidbody.AddForce(knockbackAmount * (Vector2.up + Vector2.left));
                    break;
                case FacingDirection.southWest:
                    rigidbody.AddForce(knockbackAmount * (Vector2.up + Vector2.right));
                    break;
                default:
                    break;
            }
        }
        else if (collision.CompareTag("Exit")) 
        {
            isExitingRoom = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Die
        if (health <= 0) 
        {
            Destroy(this.gameObject);
        }

        // Set Bullet Spawn Point; axis = 0.25f, directional = 0.225f
        bulletSpawnPoint = new Vector2();
        float axisPosition = 0.25f;
        float directionalPosition = 0.225f;
        switch (facingDirection) 
        {
            case FacingDirection.north:
                bulletSpawnPoint = new Vector2(0.0f, axisPosition);
                break;
            case FacingDirection.south:
                bulletSpawnPoint= new Vector2(0.0f, -axisPosition);
                break;
            case FacingDirection.east:
                bulletSpawnPoint = new Vector2(axisPosition, 0.0f);
                break;
            case FacingDirection.west:
                bulletSpawnPoint = new Vector2(-axisPosition, 0.0f);
                break;
            case FacingDirection.northEast:
                bulletSpawnPoint = new Vector3(directionalPosition, directionalPosition);
                break;
            case FacingDirection.northWest:
                bulletSpawnPoint = new Vector2(-directionalPosition, directionalPosition);
                break;
            case FacingDirection.southEast:
                bulletSpawnPoint = new Vector2(directionalPosition, -directionalPosition);
                break;
            case FacingDirection.southWest:
                bulletSpawnPoint = new Vector2(-directionalPosition, -directionalPosition);
                break;
        }
        bulletSpawnPoint += new Vector2(transform.position.x, transform.position.y);
    }

    public bool GetIsExitingRoom() 
    {
        return isExitingRoom;
    }

    public float GetHealth() 
    {
        return health;
    }

    public void SetHealth(float healthAmount) 
    {
        health += healthAmount;
    }

    public GunScriptableObject GetGun() 
    {
        return gun;
    }

    public void SetDamageMultiplier(float multiplierAmount) 
    {
        damageMultiplier = multiplierAmount;
    }

    public float GetDamageMultiplier() 
    {
        return damageMultiplier;
    }
}
