using System;
using System.Collections.Generic;
using System.Linq;
using BK9K.Game.Data.Variables;
using BK9K.Game.Extensions;
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
using OpenRpg.Genres.Fantasy.Extensions;

namespace BK9K.Game.AI.Applicators.Considerations.Local
{
    public class IsDefensiveConsiderationApplicator : DefaultLocalConsiderationApplicator<Unit>
    {
        private static readonly IClamper DefenseClamper = new Clamper(0, 30);

        public override IEnumerable<Requirement> Requirements { get; } = Array.Empty<Requirement>();

        public IsDefensiveConsiderationApplicator(IRequirementChecker<Unit> requirementChecker) : base(requirementChecker)
        {}
        
        public override IConsideration CreateConsideration(IAgent agent)
        {
            var defenseOutputAccessor = new ManualValueAccessor(() => agent.GetRelatedUnit().Stats.GetDefenseReferences().Sum(x => x.StatValue));
            return new ValueBasedConsideration(new UtilityKey(UtilityVariableTypes.IsDefensive), defenseOutputAccessor, DefenseClamper, PresetEvaluators.Linear);
        }
    }
}