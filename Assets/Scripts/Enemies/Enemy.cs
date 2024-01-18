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
    public WeaponTypeScriptableObject weapon;
    public Bullet bullet;
    public int ammo;

    public Dictionary<KnowledgeEnum, float> knowledgeBase;
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
        if (knowledgeBase[KnowledgeEnum.health] <= 0) 
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
            switch (_actionScriptableObject.id) 
            {
                case 0:
                    PaceAndShoot paceAndShoot = gameObject.AddComponent<PaceAndShoot>();
                    paceAndShoot.scriptableObject = _actionScriptableObject;
                    paceAndShoot.id = 0;
                    possibleActions.Add(paceAndShoot);
                    break;
                case 1:
                    RunIntoPlayer runIntoPlayer = gameObject.AddComponent<RunIntoPlayer>();
                    runIntoPlayer.scriptableObject = _actionScriptableObject;
                    runIntoPlayer.id = 1;
                    possibleActions.Add(runIntoPlayer);
                    break;
                case 2:
                    HideInCorner hideInCorner = gameObject.AddComponent<HideInCorner>();
                    hideInCorner.scriptableObject = _actionScriptableObject;
                    hideInCorner.id = 2;
                    possibleActions.Add(hideInCorner);
                    break;
                case 3:
                    Scream scream = gameObject.AddComponent<Scream>();
                    scream.scriptableObject = _actionScriptableObject;
                    scream.id = 3;
                    possibleActions.Add(scream);
                    break;
                default:
                    break;
            }
        }
    }

    public void SetConsiderationsAndKnowledgeBase() 
    {
        if (knowledgeBase == null) { knowledgeBase = new Dictionary<KnowledgeEnum, float>(); }
        foreach (Action action in possibleActions) 
        {
            ActionScriptableObject actionScriptableObject = null;
            foreach (ActionScriptableObject _actionScriptableObject in behaviorSet.actions) 
            {
                if (_actionScriptableObject.id == action.id)
                {
                    actionScriptableObject = _actionScriptableObject;
                }
            }
            foreach (ConsiderationScriptableObject _considerationScriptableObject in actionScriptableObject.considerationsList)
            {
                KnowledgeScriptableObject knowledgeScriptableObject = _considerationScriptableObject.knowledgeScriptableObject;
                Knowledge knowledge = new(knowledgeScriptableObject);
                SetKnowledge(knowledgeScriptableObject.knowledgeEnum, knowledge);
                SetKnowledgeValue(knowledgeScriptableObject.knowledgeEnum, knowledgeScriptableObject.startingValue);
                InputAxis inputAxis = new InputAxis(this, _considerationScriptableObject.axis, knowledge);
                Consideration consideration = new Consideration
                (
                    inputAxis,
                    _considerationScriptableObject.curveType,
                    _considerationScriptableObject.preconditions,
                    _considerationScriptableObject.slope,
                    _considerationScriptableObject.exponent,
                    _considerationScriptableObject.verticalShift,
                    _considerationScriptableObject.horizontalShift
                );
                action.SetConsideration(consideration);
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

    public float GetKnowledgeValue(KnowledgeEnum _knowledgeEnum)
    {
        return knowledgeBase[_knowledgeEnum];
    }

    public void SetKnowledge(KnowledgeEnum _knowledgeEnum, Knowledge _knowledge) 
    {
        Knowledge knowledge = new Knowledge(_knowledge.knowledgeScriptableObject);
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
        knowledge.value = BoundValueToMinMax(_value, knowledge.minValue, knowledge.maxValue);
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
    {
        KnowledgeEnum knowledgeEnum = new KnowledgeEnum();
        knowledgeEnum = _consideration.knowledgeEnum;
        InputAxis inputAxis = _consideration.inputAxis;

        float slope, exponent, verticalShift, horizontalShift, utilityValue;
        slope = _consideration.slope;
        exponent = _consideration.exponent;
        verticalShift = _consideration.verticalShift;
        horizontalShift = _consideration.horizontalShift;
        utilityValue = 0;
        switch (inputAxis.axisType)
        {
            case AxisType.percent:
                utilityValue = inputAxis.Axis(BoundValueToMinMax( knowledgeBase[knowledgeEnum],inputAxis.minValue,inputAxis.maxValue));
                break;
            case AxisType.inversePercent:
                utilityValue = inputAxis.Axis(BoundValueToMinMax(knowledgeBase[knowledgeEnum],inputAxis.minValue,inputAxis.maxValue));
                break;
            case AxisType.boolean:
                utilityValue = inputAxis.Axis(GetKnowledge(knowledgeEnum).predefinedValue, GetKnowledge(knowledgeEnum).isPredefinedValue);
                break;
            case AxisType.prefabFunction:
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
        return utilityValue;
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
