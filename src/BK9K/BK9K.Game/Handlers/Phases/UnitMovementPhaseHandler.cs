using System.Threading.Tasks;
using SystemsRx.Events;
using BK9K.Framework.Units;
using BK9K.Game.Configuration;
using BK9K.Game.Events.Units;

namespace BK9K.Game.Handlers.Phases
{
    public class UnitMovementPhaseHandler : IUnitMovementPhaseHandler
    {
        public const int PhaseTime = 500;
        public int ScaledDelay => (int)(PhaseTime * GameConfiguration.GameSpeed);

        public GameConfiguration GameConfiguration { get; }
        public IEventSystem EventSystem { get; }

        public UnitMovementPhaseHandler(IEventSystem eventSystem, GameConfiguration gameConfiguration)
        {
            EventSystem = eventSystem;
            GameConfiguration = gameConfiguration;
        }

        public async Task ExecutePhase(Unit unit)
        {
            await Task.Delay(ScaledDelay);
            EventSystem.Publish(new UnitMovingEvent(unit, unit.Position));

        }
    }
}