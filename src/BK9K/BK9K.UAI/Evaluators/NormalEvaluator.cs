using System;
using BK9K.UAI.Extensions;

namespace BK9K.UAI.Evaluators
{
    public class NormalEvaluator : IEvaluator
    {
        public float Slope { get; }
        public float YShift { get; }
        public float XShift { get; }
        public float Exponent { get; }

        public NormalEvaluator(float slope, float xShift, float yShift, float exponent)
        {
            Slope = slope;
            XShift = xShift;
            YShift = yShift;
            Exponent = exponent;
        }

        public float Evaluate(float value)
        {
            var outputValue = (float)(Slope * Math.Exp(-30.0 * Exponent * (value - XShift - 0.5) * (value - XShift - 0.5)) + YShift);
            return this.SanitizeValue(outputValue);
        }
    }
}