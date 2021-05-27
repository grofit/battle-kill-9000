using System;
using BK9K.UAI.Evaluators;

namespace BK9K.UAI.Extensions
{
    public static class IEvaluatorExtensions
    {
        public static float SanitizeValue(this IEvaluator evaluator, float value)
        {
            if(float.IsInfinity(value)) { return 0.0f; }
            if(float.IsNaN(value)) { return 0.0f; }
            if(value < 0 ) { return 0.0f; }
            if(value > 1.0f ) { return 1.0f; }

            return value;
        }
    }
}