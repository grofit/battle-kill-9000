using System;
using BK9K.UAI.Extensions;

namespace BK9K.UAI.Evaluators
{
    public class StepEvaluator : IEvaluator
    {
        public float StepValue { get; }
        public float MinValue { get; }
        public float MaxValue { get; }

        public StepEvaluator(float stepValue, float minValue = 0.0f, float maxValue = 1.0f)
        {
            StepValue = stepValue;
            MinValue = minValue;
            MaxValue = maxValue;
        }

        public float Evaluate(float value)
        { return value < StepValue ? MinValue : MaxValue; }
    }
}