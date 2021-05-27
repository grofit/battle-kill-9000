using BK9K.UAI.Evaluators;
using BK9K.UAI.Variables;

namespace BK9K.UAI.Considerations
{
    public class UtilityBasedConsideration : IUtilityBasedConsideration
    {
        public int UtilityId { get; }
        public IEvaluator Evaluator { get; }
        public IConsiderationVariables ConsiderationVariables { get; }

        public UtilityBasedConsideration(IConsiderationVariables considerationVariables, int utilityId, IEvaluator evaluator = null)
        {
            ConsiderationVariables = considerationVariables;
            UtilityId = utilityId;
            Evaluator = evaluator;
        }

        public float CalculateUtility()
        {
            var existingUtility = ConsiderationVariables[UtilityId];
            if (Evaluator == null) { return existingUtility; }
            return Evaluator.Evaluate(existingUtility);
        }
    }
}