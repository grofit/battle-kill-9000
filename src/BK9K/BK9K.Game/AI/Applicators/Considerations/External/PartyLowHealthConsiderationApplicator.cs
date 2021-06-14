using System;
using System.Collections.Generic;
using BK9K.Game.Data.Variables;
using BK9K.Game.Extensions;
using BK9K.Game.Levels;
using BK9K.Game.Units;
using BK9K.Mechanics.Units;
using BK9K.UAI;
using BK9K.UAI.Considerations;
using BK9K.UAI.Keys;
using OpenRpg.Core.Requirements;

namespace BK9K.Game.AI.Applicators.Considerations.External
{
    public class PartyLowHealthConsiderationApplicator : LevelExternalConsiderationApplicator
    {
        public override IEnumerable<Requirement> Requirements { get; } = Array.Empty<Requirement>();

        public PartyLowHealthConsiderationApplicator(IRequirementChecker<Unit> requirementChecker, Level level) : base(requirementChecker, level)
        {}

        public override bool ShouldConsiderUnit(IAgent agent, GameUnit otherUnit)
        { return otherUnit.Unit.FactionType == agent.GetRelatedUnit().FactionType; }

        public override IConsideration CreateConsideration(IAgent agent, GameUnit otherUnit)
        {
            var partyNeedsHealingUtilityKey = new UtilityKey(UtilityVariableTypes.PartyLowHealth, otherUnit.Unit.Id);
            var dependentUtilityId = new UtilityKey(UtilityVariableTypes.HasLowHealth);
            return new ExternalUtilityBasedConsideration(partyNeedsHealingUtilityKey, dependentUtilityId, () => otherUnit.Agent.UtilityVariables);
        }
    }
}