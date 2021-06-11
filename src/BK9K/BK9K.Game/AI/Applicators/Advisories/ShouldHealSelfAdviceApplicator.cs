using System.Collections.Generic;
using BK9K.Game.Data.Variables;
using BK9K.Game.Extensions;
using BK9K.Mechanics.Types;
using BK9K.Mechanics.Units;
using BK9K.UAI;
using BK9K.UAI.Advisors;
using BK9K.UAI.Advisors.Applicators;
using BK9K.UAI.Keys;
using OpenRpg.Core.Requirements;

namespace BK9K.Game.AI.Applicators.Advisories
{
    public class ShouldHealSelfAdviceApplicator : DefaultAdviceApplicator<Unit>
    {
        public override IEnumerable<Requirement> Requirements { get; } = new[]
        {
            new Requirement {RequirementType = CustomRequirementTypes.CanHealSelf}
        };
        
        public ShouldHealSelfAdviceApplicator(IRequirementChecker<Unit> requirementChecker) : base(requirementChecker)
        {}

        public override IAdvice CreateAdvice(IAgent agent)
        {
            return new DefaultAdvice(AdviceVariableTypes.HealSelf, new[]
            {
                new UtilityKey(UtilityVariableTypes.ShouldHealSelf)
            }, agent.GetRelatedUnit);
        }
    }
}