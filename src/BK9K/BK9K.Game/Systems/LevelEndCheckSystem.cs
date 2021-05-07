using System.Linq;
using SystemsRx.Attributes;
using SystemsRx.Events;
using SystemsRx.Scheduling;
using SystemsRx.Systems.Conventional;
using BK9K.Framework.Extensions;
using BK9K.Framework.Levels;
using BK9K.Game.Configuration;
using BK9K.Game.Events;
using BK9K.Game.Events.Level;
using BK9K.Game.Types;
using OpenRpg.Genres.Fantasy.Extensions;

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
            { HandlePlayerWon(); }
            else if (HasPlayerLost())
            { HandlePlayerLost(); }
        }

        public void HandlePlayerWon()
        {
            Level.HasLevelFinished = true;
            EventSystem.Publish(new LevelEndedEvent(true, GameState.LevelId++));
        }

        public void HandlePlayerLost()
        {
            Level.HasLevelFinished = true;
            GameState.PlayerUnits.ForEach(x => x.Stats.Health(x.Stats.MaxHealth()));
            var previousLevelId = GameState.LevelId;
            GameState.LevelId = 1;
            EventSystem.Publish(new LevelEndedEvent(false, previousLevelId));
        }
        
        public bool HasPlayerWon()
        { return !Level.Units?.Any(x => x.FactionType == FactionTypes.Enemy && !x.IsDead()) ?? false; }

        public bool HasPlayerLost()
        { return !Level.Units?.Any(x => x.FactionType == FactionTypes.Player && !x.IsDead()) ?? false; }
    }
}