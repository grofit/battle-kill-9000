using System;
using System.Linq;
using SystemsRx.Attributes;
using SystemsRx.Events;
using SystemsRx.Scheduling;
using SystemsRx.Systems.Conventional;
using BK9K.Framework.Extensions;
using BK9K.Framework.Units;
using BK9K.Game.Configuration;
using BK9K.Game.Events;
using BK9K.Game.Extensions;
using OpenRpg.Combat.Processors;
using OpenRpg.Core.Classes;
using OpenRpg.Genres.Fantasy.Extensions;

namespace BK9K.Game.Systems
{
    [Priority(-100)]
    public class RoundExecutionSystem : IBasicSystem
    {
        const int RoundTimeScale = 1000;
        public int RoundTimeDelay => (int)(RoundTimeScale * Configuration.GameSpeed);

        private int _elapsedRoundTime = 0;
        
        public Level Level { get; }
        public GameConfiguration Configuration { get; }
        public IEventSystem EventSystem { get; }
        public IAttackGenerator AttackGenerator { get; }
        public IAttackProcessor AttackProcessor { get; }

        public RoundExecutionSystem(Level level, IEventSystem eventSystem, GameConfiguration configuration, IAttackGenerator attackGenerator, IAttackProcessor attackProcessor)
        {
            Level = level;
            EventSystem = eventSystem;
            Configuration = configuration;
            AttackGenerator = attackGenerator;
            AttackProcessor = attackProcessor;
        }

        public void Execute(ElapsedTime elapsed)
        {
            if (Configuration.GameSpeed == 0)
            { return; }

            _elapsedRoundTime += elapsed.DeltaTime.Milliseconds;
            if (_elapsedRoundTime < RoundTimeDelay) { return; }

            PlayRound();
            EventSystem.Publish(new RoundConcludedEvent());
            _elapsedRoundTime -= RoundTimeDelay;
        }

        public void PlayRound()
        {
            Level.Units.Where(x => !x.IsDead())
                .OrderBy(x => x.Stats.Initiative())
                .ToList()
                .ForEach(TakeTurn);
        }
        
        public void TakeTurn(Unit unit)
        {
            var target = Level.Units.FirstOrDefault(x => x.FactionType != unit.FactionType && !x.IsDead());
            if (target == null) { return; }

            var processedAttack = RunAttack(unit, target);
            if (target.IsDead()) { ((DefaultClass)unit.Class).Level += 1; }
            EventSystem.Publish(new UnitAttackedEvent(unit, target, processedAttack));
        }
        
        public ProcessedAttack RunAttack(Unit attacker, Unit defender)
        {
            var attack = AttackGenerator.GenerateAttack(attacker.Stats);
            var processedAttack = AttackProcessor.ProcessAttack(attack, defender.Stats);
            var summedAttack = processedAttack.DamageDone.Sum(x => x.Value);
            var totalDamage = (int)Math.Round(summedAttack);

            if (defender.Stats.Health() >= totalDamage)
            { defender.Stats.Health(defender.Stats.Health() - totalDamage); }
            else
            { defender.Stats.Health(0); }
            return processedAttack;
        }
    }
}