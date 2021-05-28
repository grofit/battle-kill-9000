using System.Linq;
using SystemsRx.Events;
using SystemsRx.Scheduling;
using SystemsRx.Systems.Conventional;
using BK9K.Game.Configuration;
using BK9K.Game.Events.Effects;
using BK9K.Game.Levels;
using BK9K.Mechanics.Extensions;
using BK9K.Mechanics.Types;
using BK9K.Mechanics.Units;
using OpenRpg.Combat.Extensions;

namespace BK9K.Game.Systems.Effects
{
    public class EffectTimingSystem : IBasicSystem
    {
        public IEventSystem EventSystem { get; }
        public GameConfiguration GameConfiguration { get; }
        public Level Level { get; }
        
        public EffectTimingSystem(IEventSystem eventSystem, Level level, GameConfiguration gameConfiguration)
        {
            EventSystem = eventSystem;
            Level = level;
            GameConfiguration = gameConfiguration;
        }

        public void Execute(ElapsedTime elapsedTime)
        {
            var aliveUnits = Level.GameUnits.Where(x => !x.Unit.IsDead());
            foreach (var unit in aliveUnits)
            { ProcessActiveEffects(unit.Unit, elapsedTime); }
        }

        public void ProcessActiveEffects(Unit unit, ElapsedTime elapsedTime)
        {
            if(GameConfiguration.GameSpeed == GameSpeedTypes.Paused)
            { return; }

            var scaledTimePassed = (elapsedTime.DeltaTime.Milliseconds/1000.0f) / GameConfiguration.GameSpeed;

            for (var i = unit.ActiveEffects.Count - 1; i >= 0; i--)
            {
                var activeBuff = unit.ActiveEffects[i];
                activeBuff.ActiveTime += scaledTimePassed;
                activeBuff.TimeSinceTick += scaledTimePassed;

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