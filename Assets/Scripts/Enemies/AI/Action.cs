using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAxis;

public enum ActionScript { Scream, RunIntoPlayer, PaceAndShoot, HideInCorner }

public abstract class Action : MonoBehaviour
{
    public ActionScriptableObject scriptableObject;
    public List<Consideration> considerations;
    public ActionScript actionScript;

    public abstract void ExecuteAction();
    public abstract void SetConsideration(Consideration _consideration);
}
