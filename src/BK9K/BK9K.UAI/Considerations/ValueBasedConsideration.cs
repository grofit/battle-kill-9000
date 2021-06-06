using System;
using BK9K.UAI.Accessors;
using BK9K.UAI.Clampers;
using BK9K.UAI.Evaluators;
using BK9K.UAI.Keys;
using BK9K.UAI.Variables;

namespace BK9K.UAI.Considerations
{
    public class ValueBasedConsideration : IValueBasedConsideration
    {
        public UtilityKey UtilityId { get; }
        public IValueAccessor ValueAccessor { get; }
        public IClamper Clamper { get; }
        public IEvaluator Evaluator { get; }

        public ValueBasedConsideration(UtilityKey utilityId, IValueAccessor valueAccessor, IClamper clamper, IEvaluator evaluator)
        {
            ValueAccessor = valueAccessor;
            Clamper = clamper;
            Evaluator = evaluator;
            UtilityId = utilityId;
        }

        public float CalculateUtility(IUtilityVariables utilityVariables)
        {
            var value = ValueAccessor.GetValue();
            var clampedValue = Clamper.Clamp(value);
            return Evaluator.Evaluate(clampedValue);
        }
    }
}