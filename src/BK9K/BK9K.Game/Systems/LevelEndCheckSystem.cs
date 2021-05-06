using System.Linq;
using SystemsRx.Attributes;
using SystemsRx.Events;
using SystemsRx.Scheduling;
using SystemsRx.Systems.Conventional;
using BK9K.Framework.Extensions;
using BK9K.Game.Configuration;
using BK9K.Game.Events;
using BK9K.Game.Types;

namespace BK9K.Game.Systems
{
    [Priority(-100)]
    public class LevelEndCheckSystem : IBasicSystem
    {
        public IEventSystem EventSystem { get; }
        public Level Level { get; }
        public GameState GameState { get; }

        public LevelEndCheckSystem(Level level, IEventSystem eventSystem, GameState gameState)
        {
            Level = level;
            EventSystem = eventSystem;
            GameState = gameState;
        }

        public void Execute(ElapsedTime elapsed)
        {
            if (Level.Units.Count == 0 || Level.HasLevelFinished)
            { return; }

            if (HasPlayerWon())
            {
                EventSystem.Publish(new LevelEndedEvent(true, GameState.LevelId++, GameState.LevelId));
                Level.HasLevelFinished = true;

            }
            else if (HasPlayerLost())
            {
                EventSystem.Publish(new LevelEndedEvent(false, GameState.LevelId++, GameState.LevelId));
                Level.HasLevelFinished = true;
            }
        }
        
        public bool HasPlayerWon()
        { return !Level.Units?.Any(x => x.FactionType == FactionTypes.Enemy && !x.IsDead()) ?? false; }

        public bool HasPlayerLost()
        { return !Level.Units?.Any(x => x.FactionType == FactionTypes.Player && !x.IsDead()) ?? false; }
    }
}