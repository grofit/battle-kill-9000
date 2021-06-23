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
using OpenRpg.CurveFunctions.Extensions;

namespace BK9K.Game.AI.Applicators.Considerations.External
{
    public class EnemyLowHealthConsiderationApplicator : LevelExternalConsiderationApplicator
    {
        public override IEnumerable<Requirement> Requirements { get; } = Array.Empty<Requirement>();

        public EnemyLowHealthConsiderationApplicator(IRequirementChecker<Unit> requirementChecker, Level level) : base(requirementChecker, level)
        {}

        public override bool ShouldConsiderUnit(IAgent agent, GameUnit otherUnit)
        { return otherUnit.Unit.FactionType != agent.GetOwnerUnit().FactionType; }

        public override IConsideration CreateConsideration(IAgent agent, GameUnit otherUnit)
        {
            var enemyLowHealth = new UtilityKey(UtilityVariableTypes.EnemyLowHealth, otherUnit.Unit.Id);
            var dependentUtilityId = new UtilityKey(UtilityVariableTypes.HasLowHealth);
            var healthAccessor = new ManualValueAccessor(() => otherUnit.Agent.UtilityVariables.GetVariable(dependentUtilityId));
            return new ValueBasedConsideration(enemyLowHealth, healthAccessor, PresetClampers.Passthrough, PresetCurves.Linear.Alter(yShift:0.1f));
            
            
        }
    }
}