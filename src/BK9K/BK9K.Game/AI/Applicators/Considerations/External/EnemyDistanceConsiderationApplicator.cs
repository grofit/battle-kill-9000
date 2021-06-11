using System;
using System.Collections.Generic;
using System.Numerics;
using BK9K.Game.Data.Variables;
using BK9K.Game.Extensions;
using BK9K.Game.Levels;
using BK9K.Game.Units;
using BK9K.Mechanics.Types;
using BK9K.Mechanics.Units;
using BK9K.UAI;
using BK9K.UAI.Accessors;
using BK9K.UAI.Clampers;
using BK9K.UAI.Considerations;
using BK9K.UAI.Evaluators;
using BK9K.UAI.Keys;
using OpenRpg.Core.Requirements;

namespace BK9K.Game.AI.Applicators.Considerations.External
{
    public class EnemyDistanceConsiderationApplicator : LevelExternalConsiderationApplicator
    {
        private static readonly IClamper DistanceClamper = new Clamper(0, 4.0f);

        public override IEnumerable<Requirement> Requirements { get; } = Array.Empty<Requirement>();

        public EnemyDistanceConsiderationApplicator(IRequirementChecker<Unit> requirementChecker, Level level) : base(requirementChecker, level)
        {}

        public override bool ShouldConsiderUnit(IAgent agent, GameUnit otherUnit)
        { return otherUnit.Unit.FactionType != agent.GetRelatedUnit().FactionType; }

        public override IConsideration CreateConsideration(IAgent agent, GameUnit otherUnit)
        {
            var distanceUtilityKey = new UtilityKey(UtilityVariableTypes.EnemyDistance, otherUnit.Unit.Id);
            var distanceAccessor = new ManualValueAccessor(() => Vector2.Distance(otherUnit.Unit.Position, agent.GetRelatedUnit().Position));
            return new ValueBasedConsideration(distanceUtilityKey, distanceAccessor, DistanceClamper, PresetEvaluators.QuadraticLowerLeft);
        }
    }
}