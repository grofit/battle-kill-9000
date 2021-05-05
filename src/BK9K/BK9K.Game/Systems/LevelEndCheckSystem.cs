using System.Linq;
using BK9K.Framework.Extensions;
using BK9K.Framework.Types;
using BK9K.Game.Events;
using BK9K.Game.Systems.Paradigms;
using EcsRx.Attributes;
using EcsRx.Events;
using EcsRx.Scheduling;

namespace BK9K.Game.Systems
{
    [Priority(-100)]
    public class LevelEndCheckSystem : UpdateSystem
    {
        public IEventSystem EventSystem { get; }
        public World World { get; }

        public LevelEndCheckSystem(World world, IEventSystem eventSystem, IUpdateScheduler updateScheduler) : base(updateScheduler)
        {
            World = world;
            EventSystem = eventSystem;
        }

        public override void OnUpdate(ElapsedTime elapsed)
        {
            if (World.Units.Count == 0)
            { return; }

            if (HasPlayerWon())
            { EventSystem.Publish(new LevelEndedEvent(true)); }
            else if (HasPlayerLost())
            { EventSystem.Publish(new LevelEndedEvent(false)); }
        }
        
        public bool HasPlayerWon()
        { return !World.Units?.Any(x => x.FactionType == FactionTypes.Enemy && !x.IsDead()) ?? false; }

        public bool HasPlayerLost()
        { return !World.Units?.Any(x => x.FactionType == FactionTypes.Player && !x.IsDead()) ?? false; }

    }
}