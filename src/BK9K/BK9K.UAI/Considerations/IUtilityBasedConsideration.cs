using BK9K.UAI.Evaluators;
using BK9K.UAI.Keys;

namespace BK9K.UAI.Considerations
{
    public interface IUtilityBasedConsideration : IConsideration
    {
        UtilityKey DependentUtilityId { get; }
        IEvaluator Evaluator { get; }
    }
}