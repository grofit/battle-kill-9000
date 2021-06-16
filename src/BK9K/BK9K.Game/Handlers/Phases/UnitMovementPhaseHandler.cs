using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using SystemsRx.Events;
using BK9K.Game.Configuration;
using BK9K.Game.Data.Variables;
using BK9K.Game.Events.Units;
using BK9K.Game.Levels;
using BK9K.Game.Movement;
using BK9K.Mechanics.Handlers.Phases;
using BK9K.Mechanics.Units;

namespace BK9K.Game.Handlers.Phases
{
    public class UnitMovementPhaseHandler : IUnitMovementPhaseHandler
    {
        public const int PhaseTime = 500;
        public int ScaledDelay => (int)(PhaseTime * GameConfiguration.GameSpeed);

        public GameConfiguration GameConfiguration { get; }
        public IEventSystem EventSystem { get; }
        public Level Level { get; }
        public MovementAdvisor MovementAdvisor { get; } 

        public UnitMovementPhaseHandler(IEventSystem eventSystem, GameConfiguration gameConfiguration, Level level, MovementAdvisor movementAdvisor)
        {
            EventSystem = eventSystem;
            GameConfiguration = gameConfiguration;
            Level = level;
            MovementAdvisor = movementAdvisor;
        }

        public async Task ExecutePhase(Unit unit)
        {
            await Task.Delay(ScaledDelay);
            Move(unit);
            EventSystem.Publish(new UnitMovingEvent(unit, unit.Position));
        }

        public void Move(Unit unit)
        {
            var preferredTarget = FindPreferredTarget(unit);
            if (preferredTarget == null) { return; }

            var bestMovement = MovementAdvisor.GetBestMovement(unit, preferredTarget);
            unit.Position = bestMovement;
        }

        public Unit FindPreferredTarget(Unit unit)
        {
            var gameUnit = Level.GameUnits.Single(x => x.Unit == unit);
            var attackAdvice = gameUnit.Agent.AdviceHandler.GetAdvice(AdviceVariableTypes.GoAttack);
            return attackAdvice?.GetRelatedContext() as Unit;
        }
    }
}