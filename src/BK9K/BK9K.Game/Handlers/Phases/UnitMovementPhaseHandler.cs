using System.Threading.Tasks;
using SystemsRx.Events;
using BK9K.Framework.Units;
using BK9K.Game.Events.Units;

namespace BK9K.Game.Handlers.Phases
{
    public class UnitMovementPhaseHandler : IUnitMovementPhaseHandler
    {
        public IEventSystem EventSystem { get; }

        public UnitMovementPhaseHandler(IEventSystem eventSystem)
        {
            EventSystem = eventSystem;
        }

        public async Task ExecutePhase(Unit unit)
        {
            await Task.Delay(100);
            EventSystem.Publish(new UnitMovingEvent(unit, unit.Position));

        }
    }
}