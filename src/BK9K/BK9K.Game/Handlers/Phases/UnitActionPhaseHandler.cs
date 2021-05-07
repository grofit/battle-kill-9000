using System.Threading.Tasks;
using SystemsRx.Events;
using BK9K.Framework.Units;
using BK9K.Game.Data;

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
            await Task.Delay(100);

            var abilityToUse = AbilityHandlerRepository.Retrieve(unit.ActiveAbility);
            await abilityToUse.ExecuteAbility(unit);
        }
    }
}