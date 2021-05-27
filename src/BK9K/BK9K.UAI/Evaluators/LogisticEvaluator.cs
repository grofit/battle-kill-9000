using System;
using BK9K.UAI.Extensions;

namespace BK9K.UAI.Evaluators
{
    public class LogisticEvaluator : IEvaluator
    {
        public float Slope { get; }
        public float VerticalSize { get; }
        public float YShift { get; }
        public float XShift { get; }

        public LogisticEvaluator(float slope, float xShift, float yShift, float verticalSize)
        {
            Slope = slope;
            XShift = xShift;
            YShift = yShift;
            VerticalSize = verticalSize;
        }

        public float Evaluate(float value)
        {
            var outputValue = (float)(Slope / (1 + Math.Exp(-10.0 * VerticalSize * (value - 0.5 - XShift))) + YShift);
            return this.SanitizeValue(outputValue);
        }
    }
}