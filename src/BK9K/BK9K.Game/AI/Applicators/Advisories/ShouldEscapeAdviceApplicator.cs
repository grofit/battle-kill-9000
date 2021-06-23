using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using BK9K.Game.Data.Variables;
using BK9K.Game.Extensions;
using BK9K.Game.Levels;
using BK9K.Game.Movement;
using BK9K.Mechanics.Types;
using BK9K.Mechanics.Units;
using OpenRpg.AdviceEngine;
using OpenRpg.AdviceEngine.Advisors;
using OpenRpg.AdviceEngine.Advisors.Applicators;
using OpenRpg.AdviceEngine.Keys;
using OpenRpg.Core.Requirements;

namespace BK9K.Game.AI.Applicators.Advisories
{
    public class ShouldEscapeAdviceApplicator : DefaultAdviceApplicator<Unit>
    {
        public MovementAdvisor MovementAdvisor { get; }
        public Level Level { get; }

        public override IEnumerable<Requirement> Requirements { get; } = Array.Empty<Requirement>();
        
        public ShouldEscapeAdviceApplicator(IRequirementChecker<Unit> requirementChecker, MovementAdvisor movementAdvisor, Level level) : base(requirementChecker)
        {
            MovementAdvisor = movementAdvisor;
            Level = level;
        }

        public Vector2 GetBestLocation(IAgent agent)
        {
            var ownerUnit = agent.GetOwnerUnit();
            var targetUtility = agent.UtilityVariables
                .GetRelatedUtilities(UtilityVariableTypes.IsADanger)
                .OrderByDescending(x => x.Value)
                .FirstOrDefault();

            if (targetUtility.Key.UtilityId == 0)
            { return MovementAdvisor.GetBestMovementAwayFromLocation(ownerUnit, ownerUnit.Position); }

            var targetUnit = Level.GameUnits.FirstOrDefault(x => x.Agent.OwnerContext.Id == targetUtility.Key.RelatedId);
            if(targetUnit == null)
            { return MovementAdvisor.GetBestMovementAwayFromLocation(ownerUnit, ownerUnit.Position); }

            return MovementAdvisor.GetBestMovementAwayFromTarget(ownerUnit, targetUnit.Unit);
        }

        public override IAdvice CreateAdvice(IAgent agent)
        {
            return new DefaultAdvice(AdviceVariableTypes.EscapeTo, new[]
            {
                new UtilityKey(UtilityVariableTypes.IsInDanger),
                new UtilityKey(UtilityVariableTypes.HasLowHealth)
            }, () => GetBestLocation(agent));
        }
    }
}