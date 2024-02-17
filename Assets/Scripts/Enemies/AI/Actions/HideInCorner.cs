using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAxis;

public class HideInCorner : Action
{
    public GameObject[] hidingSpots;
    public GameObject hidingSpot;
    public float speed;
    private Vector2 position;

    public void Start()
    {
        speed = 1.5f;
        hidingSpots = GameObject.FindGameObjectsWithTag("HidingSpot");
        hidingSpot = hidingSpots[Random.Range(0, hidingSpots.Length)];
        position = new Vector2();
    }

    public override void ExecuteAction()
    {
        position = this.transform.position;
        position = Vector2.Lerp(position, hidingSpot.transform.position, Time.deltaTime * speed);
        this.transform.position = position;
    }
    
    public override void SetConsideration(Consideration _consideration)
    {
        considerations ??= new List<Consideration>();
        considerations.Add(_consideration);
    }
    
}
