using System;
using System.Collections.Generic;
using System.Linq;
using BK9K.Game.Data.Variables;
using BK9K.Mechanics.Units;
using OpenRpg.AdviceEngine;
using OpenRpg.AdviceEngine.Accessors;
using OpenRpg.AdviceEngine.Clampers;
using OpenRpg.AdviceEngine.Considerations;
using OpenRpg.AdviceEngine.Considerations.Applicators;
using OpenRpg.AdviceEngine.Keys;
using OpenRpg.Core.Requirements;
using OpenRpg.CurveFunctions;

namespace BK9K.Game.AI.Applicators.Considerations.Local
{
    public class IsInDangerConsideration : DefaultLocalConsiderationApplicator<Unit>
    {
        public override int Priority => ApplicatorPriorities.DependenciesOnExternal;
        
        public override IEnumerable<Requirement> Requirements { get; } = Array.Empty<Requirement>();

        public IsInDangerConsideration(IRequirementChecker<Unit> requirementChecker) : base(requirementChecker)
        {}

        public override IConsideration CreateConsideration(IAgent agent)
        {
            var dangerAccessor = new ManualValueAccessor(() =>
            {
                return agent.UtilityVariables
                    .GetRelatedUtilities(UtilityVariableTypes.IsADanger)
                    .Select(x => x.Value)
                    .DefaultIfEmpty(0)
                    .Max();
            });

            return new ValueBasedConsideration(new UtilityKey(UtilityVariableTypes.IsInDanger), dangerAccessor, PresetClampers.Passthrough, PresetCurves.PassThrough);
        }
    }
}