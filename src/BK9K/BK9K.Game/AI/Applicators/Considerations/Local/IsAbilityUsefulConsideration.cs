using System;
using System.Collections.Generic;
using System.Linq;
using BK9K.Game.Data.Repositories;
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

namespace BK9K.Game.AI.Applicators.Considerations.Local
{
    public class IsAbilityUsefulConsideration : DefaultExternalConsiderationApplicator<Unit>
    {
        private static readonly IClamper DamageClamper = new Clamper(0, 30);

        public override int Priority => ApplicatorPriorities.Local;

        public override IEnumerable<Requirement> Requirements { get; } = Array.Empty<Requirement>();

        public IAbilityHandlerRepository AbilityHandlerRepository { get; }

        public IsAbilityUsefulConsideration(IRequirementChecker<Unit> requirementChecker, IAbilityHandlerRepository abilityHandlerRepository) : base(requirementChecker)
        {
            AbilityHandlerRepository = abilityHandlerRepository;
        }

        public override IEnumerable<IConsideration> CreateConsiderations(IAgent agent)
        {
            var unit = agent.GetOwnerUnit();
            foreach (var ability in agent.GetOwnerUnit().ActiveAbilities)
            {
                var abilityHandler = AbilityHandlerRepository.Retrieve(ability.Id);
                var attackOutputAccessor = new ManualValueAccessor(() =>
                {
                    var attack = abilityHandler.CalculateAttack(unit);
                    return attack.Damages.Sum(x => x.Value);
                });
                yield return new ValueBasedConsideration(new UtilityKey(UtilityVariableTypes.IsAbilityUseful, ability.Id), attackOutputAccessor, DamageClamper, PresetCurves.Linear);
            }
        }
    }
}