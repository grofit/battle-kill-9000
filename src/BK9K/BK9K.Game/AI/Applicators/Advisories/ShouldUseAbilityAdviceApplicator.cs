using System.Collections.Generic;
using System.Linq;
using BK9K.Game.AI.Applicators.Models;
using BK9K.Game.Data.Variables;
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

        public AbilityWithTarget GetBestTarget(IHasDataId context, IUtilityVariables variables)
        {
            var targetUtility = variables
                .GetRelatedUtilities(UtilityVariableTypes.EnemyDistance)
                .OrderByDescending(x => x.Value)
                .First();

            var abilityUtility = variables
                .GetRelatedUtilities(UtilityVariableTypes.IsAbilityDamaging)
                .OrderByDescending(x => x.Value)
                .First();
            
            return new AbilityWithTarget(targetUtility.Key.RelatedId, abilityUtility.Key.RelatedId);
        }

        public override IAdvice CreateAdvice(IAgent agent)
        {
            var contextAccessor = new ManualContextAccessor(agent.OwnerContext, agent.UtilityVariables, GetBestTarget);
            return new DefaultAdvice(AdviceVariableTypes.UseAbility, new[]
            {
                new UtilityKey(UtilityVariableTypes.EnemyDistance),
                new UtilityKey(UtilityVariableTypes.EnemyLowHealth),
                new UtilityKey(UtilityVariableTypes.IsAbilityDamaging),
                new UtilityKey(UtilityVariableTypes.IsWeak),
                new UtilityKey(UtilityVariableTypes.IsVulnerable),
            }, contextAccessor);
        }
    }
}