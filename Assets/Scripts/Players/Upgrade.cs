using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    private bool canDestroy;

    // Start is called before the first frame update
    void Start()
    {
        canDestroy = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canDestroy) 
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") 
        {
            collision.gameObject.GetComponent<PlayerController>().SetDamageMultiplier(2.0f);
            canDestroy = true;
        }
    }
}
