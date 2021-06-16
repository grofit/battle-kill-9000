using System.Collections.Generic;
using System.Linq;
using BK9K.Game.Data.Variables;
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
    public class ShouldAttackAdviceApplicator : DefaultAdviceApplicator<Unit>
    {
        public Level Level { get; }
        
        public override IEnumerable<Requirement> Requirements { get; } = new[]
        {
            new Requirement { RequirementType = CustomRequirementTypes.CanAttack }
        };

        public ShouldAttackAdviceApplicator(IRequirementChecker<Unit> requirementChecker, Level level) : base(requirementChecker)
        {
            Level = level;
        }

        public Unit GetBestTarget(IAgent agent)
        {
            var targetUtility = agent.UtilityVariables
                .GetRelatedUtilities(UtilityVariableTypes.EnemyDistance)
                .OrderByDescending(x => x.Value)
                .First();

            return Level.GameUnits.Single(x => x.Unit.Id == targetUtility.Key.RelatedId).Unit;
        }

        public override IAdvice CreateAdvice(IAgent agent)
        {
            return new DefaultAdvice(AdviceVariableTypes.GoAttack, new[]
            {
                new UtilityKey(UtilityVariableTypes.EnemyDistance)
            }, () => GetBestTarget(agent));
        }
    }
}