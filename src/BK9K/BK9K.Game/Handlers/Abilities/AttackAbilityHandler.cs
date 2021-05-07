using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using SystemsRx.Events;
using BK9K.Framework.Extensions;
using BK9K.Framework.Levels;
using BK9K.Framework.Units;
using BK9K.Game.Events.Units;
using BK9K.Game.Types;
using OpenRpg.Combat.Processors;
using OpenRpg.Genres.Fantasy.Extensions;

namespace BK9K.Game.Handlers.Abilities
{
    public class AttackAbilityHandler : IUnitAbilityHandler
    {
        public int Id => AbilityTypes.Attack;

        public Level Level { get; }
        public IAttackGenerator AttackGenerator { get; }
        public IAttackProcessor AttackProcessor { get; }
        public IEventSystem EventSystem { get; }

        public AttackAbilityHandler(Level level, IAttackGenerator attackGenerator, IAttackProcessor attackProcessor, IEventSystem eventSystem)
        {
            Level = level;
            AttackGenerator = attackGenerator;
            AttackProcessor = attackProcessor;
            EventSystem = eventSystem;
        }


        public async Task ExecuteAbility(Unit unit)
        {
            var possibleTarget = FindTarget(unit);
            if (possibleTarget == null)
            { return; }

            AttackTarget(unit, possibleTarget);
        }

        public Unit FindTarget(Unit unit)
        { return Level.GetAliveUnits().FirstOrDefault(x => x.FactionType != unit.FactionType); }

        private void AttackTarget(Unit unit, Unit target)
        {
            var processedAttack = RunAttack(unit, target);
            if (target.IsDead())
            { unit.Class.Level += 1; }

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