using System;
using System.Collections.Generic;
using BK9K.Game.Data.Variables;
using BK9K.Game.Extensions;
using BK9K.Game.Levels;
using BK9K.Game.Units;
using BK9K.Mechanics.Units;
using BK9K.UAI;
using BK9K.UAI.Accessors;
using BK9K.UAI.Clampers;
using BK9K.UAI.Considerations;
using BK9K.UAI.Evaluators;
using BK9K.UAI.Extensions;
using BK9K.UAI.Keys;
using OpenRpg.Core.Requirements;

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
            
            return new ValueBasedConsideration(isDangerUtilityKey, isADangerAccessor, PresetClampers.Passthrough, PresetEvaluators.PassThrough);
        }
    }
}