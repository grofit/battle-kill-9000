using System.Threading.Tasks;
using SystemsRx.Events;
using BK9K.Game.Configuration;
using BK9K.Game.Events.Units;
using BK9K.Game.Handlers.Phases;
using BK9K.Game.Levels;
using BK9K.Mechanics.Units;

namespace BK9K.Game.Handlers
{
    public class UnitTurnHandler : IUnitTurnHandler
    {
        public Level Level { get; }
        public GameConfiguration Configuration { get; }
        public IEventSystem EventSystem { get; }
        public IUnitMovementPhaseHandler MovementPhaseHandler { get; }
        public IUnitActionPhaseHandler ActionPhaseHandler { get; }

        public UnitTurnHandler(Level level, GameConfiguration configuration, IEventSystem eventSystem, IUnitMovementPhaseHandler movementPhaseHandler, IUnitActionPhaseHandler actionPhaseHandler)
        {
            Level = level;
            Configuration = configuration;
            EventSystem = eventSystem;
            MovementPhaseHandler = movementPhaseHandler;
            ActionPhaseHandler = actionPhaseHandler;
        }

        public async Task TakeTurn(Unit unit)
        {
            EventSystem.Publish(new UnitStartTurn(unit));
            await MovementPhaseHandler.ExecutePhase(unit);
            await ActionPhaseHandler.ExecutePhase(unit);
            EventSystem.Publish(new UnitEndTurn(unit));
        }
    }
}