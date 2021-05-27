using BK9K.UAI.Evaluators;

namespace BK9K.UAI.Considerations
{
    public interface IUtilityBasedConsideration : IConsideration
    {
        int UtilityId { get; }
        IEvaluator Evaluator { get; }
    }
}