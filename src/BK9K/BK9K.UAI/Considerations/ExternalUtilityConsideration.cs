using System;
using BK9K.UAI.Evaluators;
using BK9K.UAI.Keys;
using BK9K.UAI.Variables;

namespace BK9K.UAI.Considerations
{
    public class ExternalUtilityBasedConsideration : UtilityBasedConsideration
    {
        public Func<IUtilityVariables> ExternalVariableAccessor { get; }

        public ExternalUtilityBasedConsideration(UtilityKey utilityId, UtilityKey dependentUtilityId, Func<IUtilityVariables> externalVariableAccessor, IEvaluator evaluator = null) : base(utilityId, dependentUtilityId, evaluator)
        { ExternalVariableAccessor = externalVariableAccessor; }
    }
}