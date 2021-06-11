using BK9K.UAI.Applicators;
using OpenRpg.Core.Requirements;

namespace BK9K.UAI.Considerations.Applicators
{
    public abstract class DefaultLocalConsiderationApplicator<T> : DefaultApplicator<T>, IConsiderationApplicator
    {
        public override int Priority => 1;

        protected DefaultLocalConsiderationApplicator(IRequirementChecker<T> requirementChecker) : base(requirementChecker)
        {}

        public override void ApplyTo(IAgent agent)
        {
            var consideration = CreateConsideration(agent);
            agent.ConsiderationHandler.AddConsideration(consideration);
        }

        public abstract IConsideration CreateConsideration(IAgent agent);
    }
}