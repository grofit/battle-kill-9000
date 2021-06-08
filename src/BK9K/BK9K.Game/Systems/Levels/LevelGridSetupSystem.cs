using SystemsRx.Events;
using SystemsRx.Systems.Conventional;
using BK9K.Game.Events.Level;
using BK9K.Game.Levels;
using BK9K.Mechanics.Grids;

namespace BK9K.Game.Systems.Levels
{
    public class LevelGridSetupSystem : IReactToEventSystem<RequestLevelLoadEvent>
    {
        public Level Level { get; }
        public IEventSystem EventSystem { get; }

        public LevelGridSetupSystem(Level level, IEventSystem eventSystem)
        {
            Level = level;
            EventSystem = eventSystem;
        }

        public void Process(RequestLevelLoadEvent eventData)
        {
            Level.Id = eventData.LevelId;
            Level.Grid = SetupGrid();
            Level.HasLevelFinished = false;
            Level.IsLevelLoading = true;
            EventSystem.Publish(new LevelGridSetupCompleteEvent());
        }

        public Grid SetupGrid()
        {
            return GridBuilder.Create()
                .WithSize(5, 5)
                .Build();
        }
    }
}