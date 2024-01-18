using System.Collections.Generic;
using System.Collections;
using UnityEngine;
namespace UtilityAxis
{
    #region Enums
    public enum Setting { none, demoStage }

    public enum Event { none, demoBoss }

    public enum Context { none, wait, battle }

    public enum Target { none, self, ally, enemy }

    public enum Behavior { none, patrol, engage, disengage }

    public enum Precondition { none, isInArea }

    public enum KnowledgeType { none, fixedRange, predefinedValue }

    public enum PredefinedValue { none, squadMember, squadLeader }

    public enum CurveType { none, linear, polynomial, logistic, logit, normal, sine }

    public enum AxisType { none, percent, inversePercent, boolean, prefabFunction }

    public enum PrefabFunction { none, distance }
    #endregion

    public class Knowledge
    {
        public KnowledgeScriptableObject knowledgeScriptableObject;
        public float value, minValue, maxValue;
        public PredefinedValue predefinedValue;
        public bool isPredefinedValue;

        public Knowledge(KnowledgeScriptableObject _knowledgeScriptableObject)
        {
            knowledgeScriptableObject = _knowledgeScriptableObject;
            switch (knowledgeScriptableObject.type)
            {
                case KnowledgeType.none:
                    break;
                case KnowledgeType.fixedRange:
                    value = _knowledgeScriptableObject.startingValue;
                    minValue = _knowledgeScriptableObject.minValue;
                    maxValue = _knowledgeScriptableObject.maxValue;
                    break;
                case KnowledgeType.predefinedValue:
                    predefinedValue = _knowledgeScriptableObject.predefinedValue;
                    isPredefinedValue = _knowledgeScriptableObject.isPredefinedValue;
                    break;
                default:
                    break;
            }
        }
    }

    public class InputAxis
    {
        public Enemy agent;
        public AxisType axisType;
        public Knowledge knowledge;
        public float minValue, maxValue;

        public InputAxis(Enemy _agent, AxisType _axisType, Knowledge _knowledge)
        {
            agent = _agent;
            axisType = _axisType;
            knowledge = _knowledge;
            minValue = _knowledge.minValue;
            maxValue = _knowledge.maxValue;
        }

        public float Axis(float x)
        {
            switch (axisType)
            {
                case AxisType.percent:
                    return x / maxValue;
                case AxisType.inversePercent:
                    return 1 - (x / maxValue);
                default:
                    return 0;
            }
        }

        public float Axis(PredefinedValue _predefinedValue, bool _isValue) 
        {
            if (axisType == AxisType.boolean) 
            {
                if (knowledge.predefinedValue == _predefinedValue)
                {
                    if (_isValue) { return 1; }
                    else { return 0; }
                }
                else 
                {

                    if (_isValue) { return 0; }
                    else { return 1; }
                }
            }
            return 0;
        }
    }

    public class Consideration
    {
        public float score = 0;
        public InputAxis inputAxis;
        public Knowledge knowledge;
        public KnowledgeEnum knowledgeEnum;
        public List<Precondition> preconditions;
        public CurveType curveType;
        public float slope, exponent, verticalShift, horizontalShift;

        public Consideration(InputAxis _inputAxis, CurveType _curveType, List<Precondition> _preconditions,
            float _slope, float _exponent, float _verticalShift, float _horizontalShift)
        {
            inputAxis = _inputAxis;
            knowledge = inputAxis.knowledge;
            curveType = _curveType;
            slope = _slope;
            exponent = _exponent;
            verticalShift = _verticalShift;
            horizontalShift = _horizontalShift;
            preconditions = _preconditions;
            knowledgeEnum = knowledge.knowledgeScriptableObject.knowledgeEnum;
        }
    }

    public class KnowledgeBase
    {
        public List<Knowledge> knowledges;
        public Dictionary<Knowledge, float> knowledgeBase;

        public KnowledgeBase(List<Knowledge> _knowledgeList)
        {
            foreach (Knowledge _knowledge in _knowledgeList)
            {
                knowledges.Add(SetKnowledge(_knowledge, _knowledge.value));
            }
        }

        public Knowledge SetKnowledge(Knowledge _knowledge, float _values)
        {
            knowledgeBase[_knowledge] = _values;
            return _knowledge;
        }
    }
}
