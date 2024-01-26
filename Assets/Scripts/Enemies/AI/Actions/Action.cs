using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAxis;

public abstract class Action : MonoBehaviour
{
    public ActionScriptableObject scriptableObject;
    //public List<Consideration> considerations;
    public int id;

    public abstract void ExecuteAction();
    //public abstract void SetConsideration(Consideration _consideration);
}
