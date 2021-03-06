using System;
using System.Collections.Generic;
using System.Numerics;
using BK9K.Game.Data.Variables;
using BK9K.Game.Extensions;
using BK9K.Game.Levels;
using BK9K.Game.Units;
using BK9K.Mechanics.Units;
using OpenRpg.AdviceEngine;
using OpenRpg.AdviceEngine.Accessors;
using OpenRpg.AdviceEngine.Clampers;
using OpenRpg.AdviceEngine.Considerations;
using OpenRpg.AdviceEngine.Keys;
using OpenRpg.Core.Requirements;
using OpenRpg.CurveFunctions;

namespace BK9K.Game.AI.Applicators.Considerations.External
{
    public class AllyDistanceConsiderationApplicator : LevelExternalConsiderationApplicator
    {
        private static readonly IClamper DistanceClamper = new Clamper(1.0f, 4.0f);

        public override IEnumerable<Requirement> Requirements { get; } = Array.Empty<Requirement>();

        public AllyDistanceConsiderationApplicator(IRequirementChecker<Unit> requirementChecker, Level level) : base(requirementChecker, level)
        {}

        public override bool ShouldConsiderUnit(IAgent agent, GameUnit otherUnit)
        { return otherUnit.Unit.FactionType == agent.GetOwnerUnit().FactionType; }

        public override IConsideration CreateConsideration(IAgent agent, GameUnit otherUnit)
        {
            var distanceUtilityKey = new UtilityKey(UtilityVariableTypes.AllyDistance, otherUnit.Unit.Id);
            var distanceAccessor = new ManualValueAccessor((context, _) => Vector2.Distance(otherUnit.Unit.Position, (context as Unit).Position));
            return new ValueBasedConsideration(distanceUtilityKey, distanceAccessor, DistanceClamper, PresetCurves.QuadraticLowerLeft);
        }
    }
}