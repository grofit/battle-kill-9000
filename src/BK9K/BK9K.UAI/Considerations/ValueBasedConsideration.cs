using System;
using BK9K.UAI.Accessors;
using BK9K.UAI.Clampers;
using BK9K.UAI.Evaluators;

namespace BK9K.UAI.Considerations
{
    public class ValueBasedConsideration : IValueBasedConsideration
    {
        public IValueAccessor ValueAccessor { get; }
        public IClamper Clamper { get; }
        public IEvaluator Evaluator { get; }

        public ValueBasedConsideration(IValueAccessor valueAccessor, IClamper clamper, IEvaluator evaluator)
        {
            ValueAccessor = valueAccessor;
            Clamper = clamper;
            Evaluator = evaluator;
        }

        public float CalculateUtility()
        {
            var value = ValueAccessor.GetValue();
            var clampedValue = Clamper.Clamp(value);
            return Evaluator.Evaluate(clampedValue);
        }
    }
}