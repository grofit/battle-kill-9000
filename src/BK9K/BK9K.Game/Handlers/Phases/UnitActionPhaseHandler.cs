using System.Threading.Tasks;
using SystemsRx.Events;
using BK9K.Game.Data;
using BK9K.Game.Data.Repositories;
using BK9K.Mechanics.Handlers.Phases;
using BK9K.Mechanics.Units;

namespace BK9K.Game.Handlers.Phases
{
    public class UnitActionPhaseHandler : IUnitActionPhaseHandler
    {
        public IEventSystem EventSystem { get; }
        public IAbilityHandlerRepository AbilityHandlerRepository { get; }
        
        public UnitActionPhaseHandler(IEventSystem eventSystem, IAbilityHandlerRepository abilityHandlerRepository)
        {
            EventSystem = eventSystem;
            AbilityHandlerRepository = abilityHandlerRepository;
        }

        public async Task ExecutePhase(Unit unit)
        {
            var abilityToUse = AbilityHandlerRepository.Retrieve(unit.ActiveAbilities[0].Id);
            await abilityToUse.ExecuteAbility(unit);
        }
    }
}