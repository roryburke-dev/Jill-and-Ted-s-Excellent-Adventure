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
        fireRateTimeStamp = player.GetGun().bullet.fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    void Shoot() 
    {
        fireRateTimeStamp += Time.deltaTime;
        if (Input.GetKey(player.input.shootKeyCode))
        {
            if (fireRateTimeStamp > player.GetGun().bullet.fireRate)
            {
                Bullet bulletInstance = Instantiate(bullet, player.bulletSpawnPoint, Quaternion.identity);
                bulletInstance.owner = this.gameObject;
                bulletInstance.SetValuesFromScriptableObject(player.GetGun().bullet);
                bulletInstance.SetDirection(player.facingDirection);
                bulletInstance.damage *= player.GetDamageMultiplier();
                fireRateTimeStamp = 0.0f;
            }
        }
    }
}
