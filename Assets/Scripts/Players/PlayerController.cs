using StatesEnum;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public FacingDirection facingDirection;

    public GameObject spawnPoint_N, spawnPoint_S, spawnPoint_E, spawnPoint_W, spawnPoint_NE, spawnPoint_SE, spawnPoint_NW, spawnPoint_SW;

    public Transform spawnPoint;

    public GunScriptableObject gunType;
    public float health, maxHealth;
    public float damage;
    public float knockbackAmount;

    private new Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        SetAllInActive();
        damage = gunType.bullet.damage;
        health = maxHealth;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            health -= 2.0f;
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
            GameManager gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
            Room currentRoom = gameManager.currentRoom;
            gameManager.ExitRoom(currentRoom.exitEnum);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0) 
        {
            Destroy(this.gameObject);
        }
        switch (facingDirection) 
        {
            case FacingDirection.north:
                SetAllInActive();
                spawnPoint_N.SetActive(true);
                spawnPoint = spawnPoint_N.transform;
                break;
            case FacingDirection.south:
                SetAllInActive();
                spawnPoint_S.SetActive(true);
                spawnPoint = spawnPoint_S.transform;
                break;
            case FacingDirection.east:
                SetAllInActive();
                spawnPoint_E.SetActive(true);
                spawnPoint = spawnPoint_E.transform;
                break;
            case FacingDirection.west:
                SetAllInActive();
                spawnPoint_W.SetActive(true);
                spawnPoint = spawnPoint_W.transform;
                break;
            case FacingDirection.northEast:
                SetAllInActive();
                spawnPoint_NE.SetActive(true);
                spawnPoint = spawnPoint_NE.transform;
                break;
            case FacingDirection.northWest:
                SetAllInActive();
                spawnPoint_NW.SetActive(true);
                spawnPoint = spawnPoint_NW.transform;
                break;
            case FacingDirection.southEast:
                SetAllInActive();
                spawnPoint_SE.SetActive(true);
                spawnPoint = spawnPoint_SE.transform;
                break;
            case FacingDirection.southWest:
                SetAllInActive();
                spawnPoint_SW.SetActive(true);
                spawnPoint = spawnPoint_SW.transform;
                break;
        }
    }

    void SetAllInActive() 
    {
        spawnPoint_N.SetActive(false);
        spawnPoint_S.SetActive(false);
        spawnPoint_E.SetActive(false);
        spawnPoint_W.SetActive(false);
        spawnPoint_NE.SetActive(false);
        spawnPoint_NW.SetActive(false);
        spawnPoint_SE.SetActive(false);
        spawnPoint_SW.SetActive(false);
    }
}
