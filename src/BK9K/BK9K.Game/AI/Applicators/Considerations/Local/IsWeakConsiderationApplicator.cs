using System;
using System.Collections.Generic;
using BK9K.Game.Data.Variables;
using BK9K.Mechanics.Units;
using OpenRpg.AdviceEngine;
using OpenRpg.AdviceEngine.Considerations;
using OpenRpg.AdviceEngine.Considerations.Applicators;
using OpenRpg.AdviceEngine.Keys;
using OpenRpg.Core.Requirements;
using OpenRpg.CurveFunctions;

namespace BK9K.Game.AI.Applicators.Considerations.Local
{
    public class IsWeakConsiderationApplicator : DefaultLocalConsiderationApplicator<Unit>
    {
        public override IEnumerable<Requirement> Requirements { get; } = Array.Empty<Requirement>();
        public override int Priority => ApplicatorPriorities.DependenciesOnLocal;

        public IsWeakConsiderationApplicator(IRequirementChecker<Unit> requirementChecker) : base(requirementChecker)
        { }

        public override IConsideration CreateConsideration(IAgent agent)
        {
            return new UtilityBasedConsideration(new UtilityKey(UtilityVariableTypes.IsWeak), new UtilityKey(UtilityVariableTypes.IsPowerful), PresetCurves.InverseLinear);
        }
    }
}