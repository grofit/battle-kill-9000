using System.Collections.Generic;
using OpenRpg.Core.Extensions;
using OpenRpg.Core.Requirements;

namespace BK9K.UAI.Applicators
{
    public abstract class DefaultApplicator<T> : IApplicator
    {
        public abstract IEnumerable<Requirement> Requirements { get; }
        public abstract int Priority { get; }
        
        public IRequirementChecker<T> RequirementChecker { get; }

        protected DefaultApplicator(IRequirementChecker<T> requirementChecker)
        { RequirementChecker = requirementChecker; }

        public bool CanApplyTo(IAgent agent)
        {
            var context = agent.RelatedContext;
            return RequirementChecker.AreRequirementsMet((T)context, Requirements);
        }

        public abstract void ApplyTo(IAgent agent);
    }
}