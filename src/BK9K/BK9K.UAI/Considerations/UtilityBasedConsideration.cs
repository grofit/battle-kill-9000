using BK9K.UAI.Evaluators;
using BK9K.UAI.Keys;
using BK9K.UAI.Variables;

namespace BK9K.UAI.Considerations
{
    public class UtilityBasedConsideration : IUtilityBasedConsideration
    {
        public UtilityKey UtilityId { get; }
        public UtilityKey DependentUtilityId { get; }
        public IEvaluator Evaluator { get; }

        public UtilityBasedConsideration(UtilityKey utilityId, UtilityKey dependentUtilityId, IEvaluator evaluator = null)
        {
            DependentUtilityId = dependentUtilityId;
            UtilityId = utilityId;
            Evaluator = evaluator;
        }

        public float CalculateUtility(IUtilityVariables utilityVariables)
        {
            var existingUtility = utilityVariables[UtilityId];
            if (Evaluator == null) { return existingUtility; }
            return Evaluator.Evaluate(existingUtility);
        }
    }
}