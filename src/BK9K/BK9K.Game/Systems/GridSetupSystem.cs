using BK9K.Framework.Grids;
using BK9K.Game.Events;
using EcsRx.Events;
using EcsRx.Plugins.ReactiveSystems.Custom;

namespace BK9K.Game.Systems
{
    public class GridSetupSystem : EventReactionSystem<LevelLoadingEvent>
    {
        public World World { get; }

        public GridSetupSystem(World world, IEventSystem eventSystem) : base(eventSystem)
        {
            World = world;
        }

        public override void EventTriggered(LevelLoadingEvent eventData)
        {
            World.Grid = SetupGrid();
        }

        public Grid SetupGrid()
        {
            return GridBuilder.Create()
                .WithSize(5, 5)
                .Build();
        }
    }
}