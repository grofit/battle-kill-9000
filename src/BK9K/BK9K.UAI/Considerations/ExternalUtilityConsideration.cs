using System;
using BK9K.UAI.Evaluators;
using BK9K.UAI.Variables;

namespace BK9K.UAI.Considerations
{
    public class ExternalUtilityBasedConsideration : IUtilityBasedConsideration
    {
        public int UtilityId { get; }
        public Func<IUtilityVariables> ExternalVariableAccessor { get; }
        public IEvaluator Evaluator { get; }

        public ExternalUtilityBasedConsideration(int utilityId, Func<IUtilityVariables> externalVariableAccessor, IEvaluator evaluator = null)
        {
            UtilityId = utilityId;
            ExternalVariableAccessor = externalVariableAccessor;
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