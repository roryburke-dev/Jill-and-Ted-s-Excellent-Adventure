using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAxis;

public class Scream : Action
{
    public override void ExecuteAction()
    {
        Debug.Log("AHHHHHHHHHHHH!");
    }

    public override void SetConsideration(Consideration _consideration)
    {
        considerations ??= new List<Consideration>();
        considerations.Add(_consideration);
    }
    
}
