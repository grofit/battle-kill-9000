using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemsRx.Attributes;
using SystemsRx.Events;
using SystemsRx.Scheduling;
using SystemsRx.Systems.Conventional;
using BK9K.Game.Configuration;
using BK9K.Game.Events;
using BK9K.Game.Levels;
using BK9K.Mechanics.Extensions;
using BK9K.Mechanics.Handlers;
using BK9K.Mechanics.Types;
using BK9K.Mechanics.Units;
using OpenRpg.Genres.Fantasy.Extensions;

namespace BK9K.Game.Systems.Combat
{
    [Priority(-100)]
    public class RoundExecutionSystem : IBasicSystem
    {
        const int RoundTimeScale = 1000;
        public int RoundTimeDelay => (int)(RoundTimeScale * Configuration.GameSpeed);

        private bool _isRoundActive = false;
        
        public Level Level { get; }
        public GameConfiguration Configuration { get; }
        public IEventSystem EventSystem { get; }
        public IUnitTurnHandler UnitTurnHandler { get; }

        public RoundExecutionSystem(Level level, IEventSystem eventSystem, GameConfiguration configuration, IUnitTurnHandler unitTurnHandler)
        {
            Level = level;
            EventSystem = eventSystem;
            Configuration = configuration;
            UnitTurnHandler = unitTurnHandler;
        }

        public IEnumerable<Unit> UnitsInTurnOrder => Level.GameUnits.OrderBy(x => x.Unit.Stats.Initiative()).Select(x => x.Unit);

        public void Execute(ElapsedTime elapsed)
        {
            if (_isRoundActive || Level.IsLevelLoading || Level.HasLevelFinished || Configuration.GameSpeed == GameSpeedTypes.Paused)
            { return; }

            var task = Task.Run(ProcessRound);
            
            if(task.Exception != null)
            { Console.WriteLine($"EXCEPTION: {task.Exception}");}
        }

        public async Task ProcessRound()
        {
            _isRoundActive = true;
            try
            {
                await PlayRound();
                EventSystem.Publish(new RoundConcludedEvent());
                await Task.Delay(RoundTimeDelay);
            }
            catch (Exception e)
            {
                Console.WriteLine($"EXCEPTION {e.Message} {Environment.NewLine} {Environment.NewLine} {e.StackTrace}");
            }

            _isRoundActive = false;
        }

        public async Task PlayRound()
        {
            var unitsToTakeTurns = UnitsInTurnOrder
                .Where(x => !x.IsDead())
                .ToList();

            foreach (var unit in unitsToTakeTurns)
            {
                if (unit.Stats.Health() <= 0)
                { continue; }

                await UnitTurnHandler.TakeTurn(unit);
            }
        }
    }
}