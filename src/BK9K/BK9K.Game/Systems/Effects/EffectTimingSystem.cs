using System.Linq;
using SystemsRx.Events;
using SystemsRx.Scheduling;
using SystemsRx.Systems.Conventional;
using BK9K.Framework.Extensions;
using BK9K.Framework.Levels;
using BK9K.Framework.Units;
using BK9K.Game.Events.Effects;
using OpenRpg.Combat.Extensions;

namespace BK9K.Game.Systems.Effects
{
    public class EffectTimingSystem : IBasicSystem
    {
        public IEventSystem EventSystem { get; }
        public Level Level { get; }
        
        public EffectTimingSystem(IEventSystem eventSystem, Level level)
        {
            EventSystem = eventSystem;
            Level = level;
        }

        public void Execute(ElapsedTime elapsedTime)
        {
            var aliveUnits = Level.Units.Where(x => !x.IsDead());
            foreach (var unit in aliveUnits)
            { ProcessActiveEffects(unit, elapsedTime); }
        }

        public void ProcessActiveEffects(Unit unit, ElapsedTime elapsedTime)
        {
            var millisecondsPassed = (elapsedTime.DeltaTime.Milliseconds/1000.0f);

            for (var i = unit.ActiveEffects.Count - 1; i >= 0; i--)
            {
                var activeBuff = unit.ActiveEffects[i];
                activeBuff.ActiveTime += millisecondsPassed;
                activeBuff.TimeSinceTick += millisecondsPassed;

                if (activeBuff.ActiveTime >= activeBuff.Effect.Duration)
                {
                    unit.ActiveEffects.RemoveAt(i);

                    var effectExpiredEvent = new EffectExpiredEvent { ActiveEffect = activeBuff, Unit = unit };
                    EventSystem.Publish(effectExpiredEvent);
                }

                if (activeBuff.Effect.Frequency > 0 && activeBuff.TimeSinceTick >= activeBuff.Effect.Frequency)
                {
                    activeBuff.TimeSinceTick -= activeBuff.Effect.Frequency;
                    var tickCount = activeBuff.TicksSoFar();

                    var effectExpiredEvent = new EffectTickedEvent
                    {
                        ActiveEffect = activeBuff,
                        Unit = unit,
                        TickNumber = tickCount
                    };
                    EventSystem.Publish(effectExpiredEvent);
                }
            }
        }
    }
}