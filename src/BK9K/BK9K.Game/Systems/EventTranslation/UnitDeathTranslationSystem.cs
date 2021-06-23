using SystemsRx.Events;
using SystemsRx.Systems.Conventional;
using BK9K.Game.Events.Units;
using OpenRpg.Genres.Fantasy.Extensions;

namespace BK9K.Game.Systems.EventTranslation
{
    public class UnitDeathTranslationSystem : IReactToEventSystem<UnitAttackedEvent>
    {
        public IEventSystem EventSystem { get; }
        
        public UnitDeathTranslationSystem(IEventSystem eventSystem)
        { EventSystem = eventSystem; }
        
        public void Process(UnitAttackedEvent eventData)
        {
            var hasDied = eventData.Target.Stats.Health() <= 0;
            if (hasDied)
            { EventSystem.Publish(new UnitHasDiedEvent(eventData.Target, eventData.Attacker)); }
        }
    }
}