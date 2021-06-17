using System.Collections.Generic;
using System.Linq;
using BK9K.Game.AI.Applicators.Models;
using BK9K.Game.Data.Variables;
using BK9K.Game.Extensions;
using BK9K.Game.Levels;
using BK9K.Mechanics.Types;
using BK9K.Mechanics.Units;
using OpenRpg.AdviceEngine;
using OpenRpg.AdviceEngine.Advisors;
using OpenRpg.AdviceEngine.Advisors.Applicators;
using OpenRpg.AdviceEngine.Keys;
using OpenRpg.Core.Requirements;

namespace BK9K.Game.AI.Applicators.Advisories
{
    public class ShouldUseAbilityAdviceApplicator : DefaultAdviceApplicator<Unit>
    {
        public Level Level { get; }
        
        public override IEnumerable<Requirement> Requirements { get; } = new[]
        {
            new Requirement { RequirementType = CustomRequirementTypes.CanAttack }
        };

        public ShouldUseAbilityAdviceApplicator(IRequirementChecker<Unit> requirementChecker, Level level) : base(requirementChecker)
        {
            Level = level;
        }

        public AbilityWithTarget GetBestTarget(IAgent agent)
        {
            var targetUtility = agent.UtilityVariables
                .GetRelatedUtilities(UtilityVariableTypes.EnemyDistance)
                .OrderByDescending(x => x.Value)
                .First();

            var abilityUtility = agent.UtilityVariables
                .GetRelatedUtilities(UtilityVariableTypes.IsAbilityUseful)
                .OrderByDescending(x => x.Value)
                .First();
            
            return new AbilityWithTarget(targetUtility.Key.RelatedId, abilityUtility.Key.RelatedId);
        }

        public override IAdvice CreateAdvice(IAgent agent)
        {
            return new DefaultAdvice(AdviceVariableTypes.UseAbility, new[]
            {
                new UtilityKey(UtilityVariableTypes.EnemyDistance),
                new UtilityKey(UtilityVariableTypes.IsAbilityUseful)
            }, () => GetBestTarget(agent));
        }
    }
}