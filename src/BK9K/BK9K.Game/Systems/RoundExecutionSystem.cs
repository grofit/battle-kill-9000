using System.Linq;
using BK9K.Framework.Extensions;
using BK9K.Framework.Units;
using BK9K.Game.Configuration;
using BK9K.Game.Events;
using BK9K.Game.Systems.Paradigms;
using EcsRx.Attributes;
using EcsRx.Events;
using EcsRx.Scheduling;
using OpenRpg.Core.Classes;
using OpenRpg.Genres.Fantasy.Extensions;

namespace BK9K.Game.Systems
{
    [Priority(-100)]
    public class RoundExecutionSystem : UpdateSystem
    {
        const int RoundTimeScale = 1000;
        public int RoundTimeDelay => (int)(RoundTimeScale * Configuration.GameSpeed);

        private int _elapsedRoundTime = 0;
        
        public World World { get; }
        public GameConfiguration Configuration { get; }
        public IEventSystem EventSystem { get; }

        public RoundExecutionSystem(World world, IEventSystem eventSystem, IUpdateScheduler updateScheduler, GameConfiguration configuration) : base(updateScheduler)
        {
            World = world;
            EventSystem = eventSystem;
            Configuration = configuration;
        }

        public override void OnUpdate(ElapsedTime elapsed)
        {
            _elapsedRoundTime += elapsed.DeltaTime.Milliseconds;
            if (_elapsedRoundTime < RoundTimeDelay) { return; }

            PlayRound();
            EventSystem.Publish(new RoundConcludedEvent());
            _elapsedRoundTime -= RoundTimeDelay;
        }

        public void PlayRound()
        {
            World.Units.Where(x => !x.IsDead())
                .OrderBy(x => x.Stats.Initiative())
                .ToList()
                .ForEach(TakeTurn);
        }
        
        public void TakeTurn(Unit unit)
        {
            var target = World.Units.FirstOrDefault(x => x.FactionType != unit.FactionType && !x.IsDead());
            if (target == null) { return; }

            var damage = RunAttack(unit, target);
            if (target.IsDead()) { ((DefaultClass)unit.Class).Level += 1; }
            EventSystem.Publish(new UnitAttackedEvent(unit, target, damage));
        }

        public byte GenerateAttack(Unit unit)
        { return (byte)(unit.Stats.SlashingDamage() + ((unit.Stats.SlashingDamage() / 5) * unit.Class.Level)); }

        public int RunAttack(Unit attacker, Unit defender)
        {
            var damage = GenerateAttack(attacker);
            if (defender.Stats.Health() >= damage)
            { defender.Stats.Health(defender.Stats.Health() - damage); }
            else
            { defender.Stats.Health(0); }
            return damage;
        }
    }
}