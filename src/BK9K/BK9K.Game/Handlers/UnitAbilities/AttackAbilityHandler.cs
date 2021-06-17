using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using SystemsRx.Events;
using BK9K.Game.Configuration;
using BK9K.Game.Data.Variables;
using BK9K.Game.Events.Units;
using BK9K.Game.Extensions;
using BK9K.Game.Levels;
using BK9K.Mechanics.Extensions;
using BK9K.Mechanics.Handlers;
using BK9K.Mechanics.Types.Lookups;
using BK9K.Mechanics.Units;
using OpenRpg.Combat.Attacks;
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

        public async Task<bool> ExecuteAbility(Unit unit, Unit target = null, Vector2 targetLocation = default)
        {
            if(target == null) { return false; }
            await AttackTarget(unit, target);
            return true;
        }

        public Attack CalculateAttack(Unit unit)
        { return AttackGenerator.GenerateAttack(unit.Stats); }
        
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
            var attack = CalculateAttack(attacker);
            var processedAttack = AttackProcessor.ProcessAttack(attack, defender.Stats);
            defender.ApplyDamageToTarget(processedAttack);
            return processedAttack;
        }
    }
}