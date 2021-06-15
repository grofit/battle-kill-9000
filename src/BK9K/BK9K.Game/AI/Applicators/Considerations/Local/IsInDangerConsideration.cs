using System;
using System.Collections.Generic;
using System.Linq;
using BK9K.Game.Data.Variables;
using BK9K.Mechanics.Units;
using BK9K.UAI;
using BK9K.UAI.Accessors;
using BK9K.UAI.Applicators;
using BK9K.UAI.Clampers;
using BK9K.UAI.Considerations;
using BK9K.UAI.Considerations.Applicators;
using BK9K.UAI.Evaluators;
using BK9K.UAI.Keys;
using OpenRpg.Core.Requirements;

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

            return new ValueBasedConsideration(new UtilityKey(UtilityVariableTypes.IsInDanger), dangerAccessor, PresetClampers.Passthrough, PresetEvaluators.PassThrough);
        }
    }
}