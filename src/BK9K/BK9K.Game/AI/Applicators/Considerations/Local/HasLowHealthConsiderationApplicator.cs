using System;
using System.Collections.Generic;
using BK9K.Game.Data.Variables;
using BK9K.Game.Extensions;
using BK9K.Mechanics.Units;
using OpenRpg.AdviceEngine;
using OpenRpg.AdviceEngine.Accessors;
using OpenRpg.AdviceEngine.Clampers;
using OpenRpg.AdviceEngine.Considerations;
using OpenRpg.AdviceEngine.Considerations.Applicators;
using OpenRpg.AdviceEngine.Keys;
using OpenRpg.Core.Requirements;
using OpenRpg.CurveFunctions;
using OpenRpg.Genres.Fantasy.Extensions;

namespace BK9K.Game.AI.Applicators.Considerations.Local
{
    public class HasLowHealthConsiderationApplicator : DefaultLocalConsiderationApplicator<Unit>
    {
        public override IEnumerable<Requirement> Requirements { get; } = Array.Empty<Requirement>();

        public HasLowHealthConsiderationApplicator(IRequirementChecker<Unit> requirementChecker) : base(requirementChecker)
        {}
        
        public override IConsideration CreateConsideration(IAgent agent)
        {
            var healthValueAccessor = new ManualValueAccessor(() => agent.GetOwnerUnit().Stats.Health());
            var healthClamper = new DynamicClamper(() => 0, () => agent.GetOwnerUnit().Stats.MaxHealth());
            return new ValueBasedConsideration(new UtilityKey(UtilityVariableTypes.HasLowHealth), healthValueAccessor, healthClamper, PresetCurves.InverseLinear);
        }
    }
}