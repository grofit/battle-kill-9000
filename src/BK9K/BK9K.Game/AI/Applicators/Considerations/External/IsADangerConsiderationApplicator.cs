using System;
using System.Collections.Generic;
using BK9K.Game.Data.Variables;
using BK9K.Game.Extensions;
using BK9K.Game.Levels;
using BK9K.Game.Units;
using BK9K.Mechanics.Units;
using OpenRpg.AdviceEngine;
using OpenRpg.AdviceEngine.Accessors;
using OpenRpg.AdviceEngine.Clampers;
using OpenRpg.AdviceEngine.Considerations;
using OpenRpg.AdviceEngine.Extensions;
using OpenRpg.AdviceEngine.Keys;
using OpenRpg.Core.Requirements;
using OpenRpg.CurveFunctions;

namespace BK9K.Game.AI.Applicators.Considerations.External
{
    public class IsADangerConsiderationApplicator : LevelExternalConsiderationApplicator
    {
        public override IEnumerable<Requirement> Requirements { get; } = Array.Empty<Requirement>();

        public IsADangerConsiderationApplicator(IRequirementChecker<Unit> requirementChecker, Level level) : base(requirementChecker, level)
        {}

        public override bool ShouldConsiderUnit(IAgent agent, GameUnit otherUnit)
        { return otherUnit.Unit.FactionType != agent.GetRelatedUnit().FactionType; }

        public override IConsideration CreateConsideration(IAgent agent, GameUnit otherUnit)
        {
            var isDangerUtilityKey = new UtilityKey(UtilityVariableTypes.IsADanger, otherUnit.Unit.Id);
            var enemyDistanceUtilityKey = new UtilityKey(UtilityVariableTypes.EnemyDistance, otherUnit.Unit.Id);
            var isADangerAccessor = new ManualValueAccessor(() =>
            {
                var distanceUtility = agent.UtilityVariables.GetVariable(enemyDistanceUtilityKey);
                var powerfulUtility = otherUnit.Agent.UtilityVariables.GetVariable(new UtilityKey(UtilityVariableTypes.IsPowerful));
                return UtilityExtensions.CalculateScore(distanceUtility, powerfulUtility);
            });
            
            return new ValueBasedConsideration(isDangerUtilityKey, isADangerAccessor, PresetClampers.Passthrough, PresetCurves.PassThrough);
        }
    }
}