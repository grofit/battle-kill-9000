using System;
using BK9K.UAI.Accessors;
using BK9K.UAI.Clampers;
using BK9K.UAI.Evaluators;

namespace BK9K.UAI.Considerations
{
    public interface IValueBasedConsideration : IConsideration
    {
        IValueAccessor ValueAccessor { get; }
        IClamper Clamper { get; }
        IEvaluator Evaluator { get; }
    }
}