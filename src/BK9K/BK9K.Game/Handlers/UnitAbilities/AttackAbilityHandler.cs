using System.Linq;
using System.Threading.Tasks;
using SystemsRx.Events;
using BK9K.Game.Configuration;
using BK9K.Game.Events.Units;
using BK9K.Game.Extensions;
using BK9K.Game.Levels;
using BK9K.Mechanics.Extensions;
using BK9K.Mechanics.Handlers;
using BK9K.Mechanics.Types.Lookups;
using BK9K.Mechanics.Units;
using OpenRpg.Combat.Processors;

namespace BK9K.Game.Handlers.UnitAbilities
{
    public class AttackAbilityHandler : IAbilityHandler
    {
        public const int AttackActionTime = 500;
        public int ScaledAttackDelay => (int) (AttackActionTime * GameConfiguration.GameSpeed);

        public int Id => AbilityLookups.Attack;

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

        public async Task<bool> ExecuteAbility(Unit unit)
        {
            var possibleTarget = FindTarget(unit);
            if (possibleTarget == null)
            {
                return false;
            }

            await AttackTarget(unit, possibleTarget);
            return true;
        }

        public Unit FindTarget(Unit unit)
        {
            var possibleUnit = Level.GetAliveUnits().FirstOrDefault(x => x.Unit.FactionType != unit.FactionType);
            return possibleUnit?.Unit;
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
            defender.ApplyDamageToTarget(processedAttack);
            return processedAttack;
        }
    }
}