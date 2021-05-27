using System;
using BK9K.UAI.Extensions;

namespace BK9K.UAI.Evaluators
{
    public class SineEvaluator : IEvaluator
    {
        public float Slope { get; }
        public float YShift { get; }
        public float XShift { get; }

        public SineEvaluator(float slope, float xShift, float yShift)
        {
            Slope = slope;
            XShift = xShift;
            YShift = yShift;
        }

        public float Evaluate(float value)
        {
            var outputValue = (float)(0.5 * Slope * Math.Sin(2.0 * Math.PI * (value - XShift)) + 0.5 + YShift);
            return this.SanitizeValue(outputValue);
        }
    }
}