using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UtilityAxis;

public class Enemy : MonoBehaviour
{
    
    public GameObject prefab;
    public EnemyScriptableObject scriptableObject;
    public BehaviorSetScriptableObject behaviorSet;
    public GunScriptableObject gun;
    public Bullet bullet;
    public int ammo;
    
    public Dictionary<KnowledgeEnum, Vector3> knowledgeBase;
    
    public List<Knowledge> knowledgeContext;
    public List<Action> possibleActions;
    public Action winningAction;

    private GameObject loot;
    private bool canDestroy;

    // Start is called before the first frame update
    void Start()
    {
        SetActions();
        SetConsiderationsAndKnowledgeBase();
        loot = scriptableObject.loot;
        canDestroy = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        CalculateWinningAction();
        winningAction.ExecuteAction();
        if (knowledgeBase[KnowledgeEnum.health][0] <= 0) 
        {
            if (loot)
            {
                DropLoot();
            }
            else 
            {
                canDestroy = true;
            }
        }
        if (canDestroy) 
        {
            Destroy(this.gameObject);
        }
    }
    void DropLoot() 
    {
        Instantiate(loot, this.transform.position, Quaternion.identity);
        canDestroy = true;
    }

    void SetActions() 
    {
        possibleActions ??= new List<Action>();
        foreach (ActionScriptableObject _actionScriptableObject in behaviorSet.actions)
        {
            switch (_actionScriptableObject.actionScript) 
            {
                case ActionScript.PaceAndShoot:
                    PaceAndShoot paceAndShoot = gameObject.AddComponent<PaceAndShoot>();
                    paceAndShoot.scriptableObject = _actionScriptableObject;
                    possibleActions.Add(paceAndShoot);
                    break;
                case ActionScript.RunIntoPlayer:
                    RunIntoPlayer runIntoPlayer = gameObject.AddComponent<RunIntoPlayer>();
                    runIntoPlayer.scriptableObject = _actionScriptableObject;
                    possibleActions.Add(runIntoPlayer);
                    break;
                case ActionScript.HideInCorner:
                    HideInCorner hideInCorner = gameObject.AddComponent<HideInCorner>();
                    hideInCorner.scriptableObject = _actionScriptableObject;
                    possibleActions.Add(hideInCorner);
                    break;
                case ActionScript.Scream:
                    Scream scream = gameObject.AddComponent<Scream>();
                    scream.scriptableObject = _actionScriptableObject;
                    possibleActions.Add(scream);
                    break;
                default:
                    break;
            }
        }
    }

    public void SetConsiderationsAndKnowledgeBase() 
    {
        if (knowledgeBase == null) { knowledgeBase = new Dictionary<KnowledgeEnum, Vector3>(); }
        foreach (Action action in possibleActions) 
        {
            ActionScriptableObject actionScriptableObject = null;
            foreach (ActionScriptableObject _actionScriptableObject in behaviorSet.actions) 
            {
                if (_actionScriptableObject.actionScript == action.actionScript)
                {
                    actionScriptableObject = _actionScriptableObject;
                }
            }
            foreach (ConsiderationScriptableObject _considerationScriptableObject in actionScriptableObject.considerations)
            {
                KnowledgeScriptableObject knowledgeScriptableObject = _considerationScriptableObject.knowledgeScriptableObject;
                Knowledge knowledge = new(knowledgeScriptableObject, this);
                SetKnowledge(knowledgeScriptableObject.knowledgeEnum, knowledge);
                SetKnowledgeValue(knowledgeScriptableObject.knowledgeEnum, knowledge.value[1]);
                //Consideration consideration = 
                //action.SetConsideration(consideration);
               
            }
        }
    }
  
    public Knowledge GetKnowledge(KnowledgeEnum _knowledgeEnum) 
    {
        if (knowledgeContext == null) { return null; }
        foreach (Knowledge _knowledge in knowledgeContext) 
        {
            if (_knowledge.knowledgeScriptableObject.knowledgeEnum == _knowledgeEnum) 
            {
                return _knowledge; 
            }
        }
        return null;
     }

    public Vector3 GetKnowledgeValue(KnowledgeEnum _knowledgeEnum)
    {
        return knowledgeBase[_knowledgeEnum];
    }

    public void SetKnowledge(KnowledgeEnum _knowledgeEnum, Knowledge _knowledge) 
    {
        Knowledge knowledge = new Knowledge(_knowledge.knowledgeScriptableObject, this);
        if (knowledgeContext == null)
        {
            knowledgeContext = new List<Knowledge> ();
            knowledgeContext.Add(knowledge);
        }
        else 
        {
            bool isInKnowledgeContext = false;
            List<Knowledge> knowledgeList = new List<Knowledge>();
            knowledgeList = knowledgeContext;
            Knowledge knowledgeToRemove = null;
            foreach (Knowledge _knowledgeFromContext in knowledgeList)
            {
                if (_knowledgeFromContext.knowledgeScriptableObject.knowledgeEnum == _knowledgeEnum)
                {
                    knowledgeToRemove = _knowledgeFromContext;
                    isInKnowledgeContext = true;
                }
            }
            if (!isInKnowledgeContext)
            {
                knowledgeContext.Add(knowledge);
            }
            else 
            {
                knowledgeContext.Remove(knowledgeToRemove);
                knowledgeContext.Add(knowledge);
            }
        }
    }

    public void SetKnowledgeValue(KnowledgeEnum _knowledgeEnum, float _value) 
    {
        Knowledge knowledge = GetKnowledge(_knowledgeEnum);
        Vector3 knowledgeValue = GetKnowledgeValue(_knowledgeEnum);
        knowledge.value[0] = BoundValueToMinMax(knowledgeValue[0], knowledgeValue[1], knowledgeValue[2]);
        knowledgeBase[_knowledgeEnum] = knowledge.value;
        SetKnowledge(_knowledgeEnum, knowledge);
    }

    float BoundValueToMinMax(float _value, float _min, float _max)
    {
        if (_value < _min) _value = _min;
        if (_value > _max) _value = _max;
        return _value;
    }

    public float CalculateUtility(List<Consideration> _considerationsList)
    {
        float utility = 0;
        foreach (Consideration _consideration in _considerationsList)
        {
            utility = utility + CalculateUtilityPerConsideration(_consideration);
        }
        return utility;
    }

    float CalculateUtilityPerConsideration(Consideration _consideration)
    {/*
        InputAxis inputAxis = _consideration.inputAxis;

        float slope, exponent, verticalShift, horizontalShift, utilityValue;
        slope = _consideration.slope;
        exponent = _consideration.exponent;
        verticalShift = _consideration.verticalShift;
        horizontalShift = _consideration.horizontalShift;
        utilityValue = 0;
        Vector3 knowledgeValue = GetKnowledgeValue(knowledgeEnum);
        switch (inputAxis.axisType)
        {
            case AxisType.percent:
                utilityValue = inputAxis.Axis(BoundValueToMinMax(knowledgeValue[0], knowledgeValue[1], knowledgeValue[2]));
                break;
            case AxisType.inversePercent:
                utilityValue = inputAxis.Axis(BoundValueToMinMax(knowledgeValue[0], knowledgeValue[1], knowledgeValue[2]));
                break;
            case AxisType.boolean:
                utilityValue = inputAxis.Axis(GetKnowledge(knowledgeEnum).predefinedValue, GetKnowledge(knowledgeEnum).isPredefinedValue);
                break;
            case AxisType.prefabFunction:
                switch (GetKnowledge(knowledgeEnum).prefabFunction) 
                {
                    case PrefabFunction.none:
                        break;
                    case PrefabFunction.distance:
                        // Distance Formula
                        break;
                }
                break;
            default:
               break;
        }
        switch (_consideration.curveType)
        {
            case CurveType.linear:
                utilityValue -= verticalShift;
                utilityValue *= slope;
                utilityValue += horizontalShift;
                break;
            default:
                break;
        }
        return utilityValue;*/
        return 0.0f;

    }


    public void CalculateWinningAction()
    {
        float localMaxUtility = -1;
        foreach (Action _action in possibleActions)
        {
            float actionUtility = CalculateUtility(_action.considerations);
            if (actionUtility > localMaxUtility)
            {
                winningAction = _action;
                localMaxUtility = actionUtility;
            }
        }
    }
    
}
