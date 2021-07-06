using System;
using System.Collections.Generic;
using System.Linq;
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
    public class IsPowerfulConsiderationApplicator : DefaultLocalConsiderationApplicator<Unit>
    {
        private static readonly IClamper DamageClamper = new Clamper(1, 15);
        
        public override IEnumerable<Requirement> Requirements { get; } = Array.Empty<Requirement>();

        public IsPowerfulConsiderationApplicator(IRequirementChecker<Unit> requirementChecker) : base(requirementChecker)
        {}
        
        public override IConsideration CreateConsideration(IAgent agent)
        {
            var attackOutputAccessor = new ManualValueAccessor((context, _) => (context as Unit).Stats.GetDamageReferences().Sum(x => x.StatValue));
            return new ValueBasedConsideration(new UtilityKey(UtilityVariableTypes.IsPowerful), attackOutputAccessor, DamageClamper, PresetCurves.Linear);
        }
    }
}