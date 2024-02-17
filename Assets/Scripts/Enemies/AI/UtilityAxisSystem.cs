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
        public Vector3 value;
        public bool isPredefinedValue;
        public PredefinedValue predefinedValue;
        public bool usingPrefabFunction;
        public PrefabFunction prefabFunction;

        public Knowledge(KnowledgeScriptableObject _knowledgeScriptableObject, Enemy agent)
        {
            knowledgeScriptableObject = _knowledgeScriptableObject;
            switch (knowledgeScriptableObject.knowledgeType)
            {
                case KnowledgeType.none:
                    break;
                case KnowledgeType.fixedRange:
                    value = agent.GetKnowledgeValue(knowledgeScriptableObject.knowledgeEnum);
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
        public Knowledge knowledge;
        public AxisType axisType;
        public CurveType curveType;
        public float slope, exponent, horizontalShift, verticalShift;
        public float minValue, maxValue;

        public InputAxis(InputAxisScriptableObject _inputAxisScriptableObject, Knowledge _knowledge)
        {
            axisType = _inputAxisScriptableObject.axis;
            curveType = _inputAxisScriptableObject.curveType;
            slope = _inputAxisScriptableObject.slope;
            exponent = _inputAxisScriptableObject.exponent;
            horizontalShift = _inputAxisScriptableObject.horizontalShift;
            verticalShift = _inputAxisScriptableObject.verticalShift;
            knowledge = _knowledge;
            minValue = knowledge.value[1];
            maxValue = knowledge.value[2];
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
        public List<Precondition> preconditions;
        public List<InputAxis> inputAxis;

        public Consideration(ConsiderationScriptableObject _considerationScriptableObject) 
        {
            foreach (InputAxisScriptableObject inputAxisScriptableObject in _considerationScriptableObject.inputAxes) 
            {
              //  inputAxis.Add(new InputAxis(inputAxisScriptableObject));
            }
        }
    }

    public class KnowledgeBase
    {
        public List<Knowledge> knowledges;
        public Dictionary<Knowledge, Vector3> knowledgeBase;

        public KnowledgeBase(List<Knowledge> _knowledgeList)
        {
            foreach (Knowledge _knowledge in _knowledgeList)
            {
                knowledges.Add(SetKnowledge(_knowledge, _knowledge.value));
            }
        }

        public Knowledge SetKnowledge(Knowledge _knowledge, Vector3 _values)
        {
            knowledgeBase[_knowledge] = _values;
            return _knowledge;
        }
    }
    
}