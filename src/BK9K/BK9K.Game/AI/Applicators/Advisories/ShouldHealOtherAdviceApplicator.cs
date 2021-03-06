using System.Collections.Generic;
using System.Linq;
using BK9K.Game.Data.Variables;
using BK9K.Game.Extensions;
using BK9K.Game.Levels;
using BK9K.Mechanics.Types;
using BK9K.Mechanics.Units;
using OpenRpg.AdviceEngine;
using OpenRpg.AdviceEngine.Accessors;
using OpenRpg.AdviceEngine.Advisors;
using OpenRpg.AdviceEngine.Advisors.Applicators;
using OpenRpg.AdviceEngine.Keys;
using OpenRpg.AdviceEngine.Variables;
using OpenRpg.Core.Common;
using OpenRpg.Core.Requirements;

namespace BK9K.Game.AI.Applicators.Advisories
{
    public class ShouldHealOtherAdviceApplicator : DefaultAdviceApplicator<Unit>
    {
        public Level Level { get; }
        
        public override IEnumerable<Requirement> Requirements { get; } = new[]
        {
            new Requirement { RequirementType = CustomRequirementTypes.CanHealOthers }
        };
        
        public ShouldHealOtherAdviceApplicator(IRequirementChecker<Unit> requirementChecker, Level level) : base(requirementChecker)
        {
            Level = level;
        }
        
        public Unit GetBestTarget(IHasDataId context, IUtilityVariables variables)
        {
            var targetUtility = variables
                .GetRelatedUtilities(UtilityVariableTypes.PartyLowHealth)
                .OrderByDescending(x => x.Value)
                .FirstOrDefault();

            if (targetUtility.Key.UtilityId == 0)
            { return null; }

            return Level.GameUnits.Single(x => x.Unit.Id == targetUtility.Key.RelatedId).Unit;
        }

        public override IAdvice CreateAdvice(IAgent agent)
        {
            var contextAccessor = new ManualContextAccessor(agent.OwnerContext, agent.UtilityVariables, GetBestTarget);
            return new DefaultAdvice(AdviceVariableTypes.HealOther, new[]
            {
                new UtilityKey(UtilityVariableTypes.PartyLowHealth),
                new UtilityKey(UtilityVariableTypes.AllyDistance),
            }, contextAccessor);
        }
    }
}