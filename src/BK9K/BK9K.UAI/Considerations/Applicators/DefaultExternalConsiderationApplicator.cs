using System.Collections.Generic;
using BK9K.UAI.Applicators;
using OpenRpg.Core.Requirements;

namespace BK9K.UAI.Considerations.Applicators
{
    public abstract class DefaultExternalConsiderationApplicator<T> : DefaultApplicator<T>, IConsiderationApplicator
    {
        public override int Priority => 2;

        protected DefaultExternalConsiderationApplicator(IRequirementChecker<T> requirementChecker) : base(requirementChecker)
        {}

        public override void ApplyTo(IAgent agent)
        {
            var considerations = CreateConsiderations(agent);
            foreach (var consideration in considerations)
            { agent.ConsiderationHandler.AddConsideration(consideration); }
        }

        public abstract IEnumerable<IConsideration> CreateConsiderations(IAgent agent);
    }
}