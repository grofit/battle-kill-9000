using SystemsRx.Events;
using SystemsRx.Systems.Conventional;
using BK9K.Game.Events.Level;
using BK9K.Game.Levels;
using BK9K.Game.Processors;

namespace BK9K.Game.Systems.Levels
{
    public class LevelLoadingSystem : IReactToEventSystem<RequestLevelLoadEvent>
    {
        public Level Level { get; }
        public IEventSystem EventSystem { get; }
        public IProcessorRegistry<Level> LevelProcessors { get; }
        
        public LevelLoadingSystem(Level level, IEventSystem eventSystem, IProcessorRegistry<Level> levelProcessors)
        {
            Level = level;
            EventSystem = eventSystem;
            LevelProcessors = levelProcessors;
        }

        public void Process(RequestLevelLoadEvent eventData)
        {
            Level.Id = eventData.LevelId;
            Level.HasLevelFinished = false;
            Level.IsLevelLoading = true;

            LevelProcessors.Process(Level).ContinueWith(_ =>
            {
                Level.IsLevelLoading = false;
                EventSystem.Publish(new LevelLoadedEvent());
            });
        }
    }
}