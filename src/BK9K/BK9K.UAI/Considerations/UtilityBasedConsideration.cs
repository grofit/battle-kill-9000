using BK9K.UAI.Evaluators;
using BK9K.UAI.Variables;

namespace BK9K.UAI.Considerations
{
    public class UtilityBasedConsideration : IUtilityBasedConsideration
    {
        public int UtilityId { get; }
        public IEvaluator Evaluator { get; }

        public UtilityBasedConsideration(int utilityId, IEvaluator evaluator = null)
        {
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