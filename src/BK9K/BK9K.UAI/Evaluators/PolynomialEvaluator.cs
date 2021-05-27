using System;
using BK9K.UAI.Extensions;

namespace BK9K.UAI.Evaluators
{
    public class PolynomialEvaluator : IEvaluator
    {
        public float Slope { get; }
        public float Exponent { get; }
        public float YShift { get; }
        public float XShift { get; }

        public PolynomialEvaluator(float slope, float xShift, float yShift, float exponent)
        {
            Slope = slope;
            XShift = xShift;
            YShift = yShift;
            Exponent = exponent;
        }

        public float Evaluate(float value)
        {
            var outputValue = Slope * (float)Math.Pow((value - XShift), Exponent) + YShift;
            return this.SanitizeValue(outputValue);
        }
    }
}