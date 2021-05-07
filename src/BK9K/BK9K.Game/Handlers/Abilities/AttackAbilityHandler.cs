using System;
using System.Linq;
using System.Threading.Tasks;
using SystemsRx.Events;
using BK9K.Framework.Extensions;
using BK9K.Framework.Levels;
using BK9K.Framework.Units;
using BK9K.Game.Configuration;
using BK9K.Game.Events.Units;
using BK9K.Game.Types;
using OpenRpg.Combat.Processors;
using OpenRpg.Genres.Fantasy.Extensions;

namespace BK9K.Game.Handlers.Abilities
{
    public class AttackAbilityHandler : IUnitAbilityHandler
    {
        public const int AttackActionTime = 500;
        public int ScaledAttackDelay => (int) (AttackActionTime * GameConfiguration.GameSpeed);

        public int Id => AbilityTypes.Attack;

        public Level Level { get; }
        public GameConfiguration GameConfiguration { get; }
        public IAttackGenerator AttackGenerator { get; }
        public IAttackProcessor AttackProcessor { get; }
        public IEventSystem EventSystem { get; }


        public AttackAbilityHandler(Level level, IAttackGenerator attackGenerator, IAttackProcessor attackProcessor,
            IEventSystem eventSystem, GameConfiguration gameConfiguration)
        {
            Level = level;
            AttackGenerator = attackGenerator;
            AttackProcessor = attackProcessor;
            EventSystem = eventSystem;
            GameConfiguration = gameConfiguration;
        }

        public async Task ExecuteAbility(Unit unit)
        {
            var possibleTarget = FindTarget(unit);
            if (possibleTarget == null)
            {
                return;
            }

            await AttackTarget(unit, possibleTarget);
        }

        public Unit FindTarget(Unit unit)
        {
            return Level.GetAliveUnits().FirstOrDefault(x => x.FactionType != unit.FactionType);
        }

        private async Task AttackTarget(Unit unit, Unit target)
        {
            var processedAttack = RunAttack(unit, target);
            EventSystem.Publish(new UnitAttackedEvent(unit, target, processedAttack));

            await Task.Delay(ScaledAttackDelay);

            if (target.IsDead())
            {
                unit.Class.Level += 1;
            }
        }

        public ProcessedAttack RunAttack(Unit attacker, Unit defender)
        {
            var attack = AttackGenerator.GenerateAttack(attacker.Stats);
            var processedAttack = AttackProcessor.ProcessAttack(attack, defender.Stats);
            var summedAttack = processedAttack.DamageDone.Sum(x => x.Value);
            var totalDamage = (int) Math.Round(summedAttack);

            if (defender.Stats.Health() >= totalDamage)
            {
                defender.Stats.Health(defender.Stats.Health() - totalDamage);
            }
            else
            {
                defender.Stats.Health(0);
            }

            return processedAttack;
        }
    }
}