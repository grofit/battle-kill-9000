using System;
using BK9K.UAI.Extensions;

namespace BK9K.UAI.Evaluators
{
    public class LogitEvaluator : IEvaluator
    {
        public float Slope { get; }
        public float YShift { get; }
        public float XShift { get; }

        public LogitEvaluator(float slope, float xShift, float yShift)
        {
            Slope = slope;
            XShift = xShift;
            YShift = yShift;
        }

        public float Evaluate(float value)
        {
            var outputValue = (float)(Slope * Math.Log((value - XShift) / (1.0 - (value - XShift))) / 5.0 + 0.5 + YShift);
            return this.SanitizeValue(outputValue);
        }
    }
}