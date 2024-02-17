using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UtilityAxis;

public class RunIntoPlayer : Action
{
    public Transform player;
    public float speed;
    private Vector2 position;

    public void Start()
    {
        speed = 0.5f;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        position = new Vector2();
    }

    public override void ExecuteAction()
    {
        player ??= GameObject.FindGameObjectWithTag("Player").transform;
        if (player) 
        {
            position = this.transform.position;
            position = Vector2.Lerp(position, player.position, Time.deltaTime * speed);
            this.transform.position = position;
        }
    }

    public override void SetConsideration(Consideration _consideration)
    {
        considerations ??= new List<Consideration>();
        considerations.Add(_consideration);
    }
    
}
