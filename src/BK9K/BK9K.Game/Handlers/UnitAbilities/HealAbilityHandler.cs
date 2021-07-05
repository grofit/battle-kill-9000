using System;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using SystemsRx.Events;
using BK9K.Game.Configuration;
using BK9K.Game.Events.Units;
using BK9K.Game.Levels;
using BK9K.Mechanics.Extensions;
using BK9K.Mechanics.Handlers;
using BK9K.Mechanics.Types.Lookups;
using BK9K.Mechanics.Units;
using OpenRpg.Combat.Attacks;
using OpenRpg.Combat.Processors;
using OpenRpg.Genres.Fantasy.Extensions;
using OpenRpg.Genres.Fantasy.Types;

namespace BK9K.Game.Handlers.UnitAbilities
{
    public class HealAbilityHandler : IAbilityHandler
    {
        public const int AttackActionTime = 500;
        public int ScaledAttackDelay => (int) (AttackActionTime * GameConfiguration.GameSpeed);

        public int Id => AbilityLookups.Heal;
        public float BaseHealScore = 5;

        public Level Level { get; }
        public GameConfiguration GameConfiguration { get; }
        public IAttackGenerator AttackGenerator { get; }
        public IAttackProcessor AttackProcessor { get; }
        public IEventSystem EventSystem { get; }
        
        public HealAbilityHandler(Level level, IAttackGenerator attackGenerator, IAttackProcessor attackProcessor,
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
            await HealTarget(unit, target);
            return true;
        }

        public Attack WrapHealIntoAttack(float healScore)
        {  return new Attack(new []{ new Damage(DamageTypes.LightDamage, healScore) }); }

        public Attack CalculateAttack(Unit unit)
        {
            var lightDamage = unit.Stats.LightDamage();
            if (lightDamage == 0)
            { return WrapHealIntoAttack(BaseHealScore); }
            
            var wisdom = unit.Stats.Wisdom();
            if (wisdom == 0)
            { return WrapHealIntoAttack(BaseHealScore); }
            
            var modifier = ((float)wisdom / 75);
            return WrapHealIntoAttack(BaseHealScore + (lightDamage * modifier));
        }
        
        private async Task HealTarget(Unit unit, Unit target)
        {
            var processedAttack = RunHeal(unit, target);
            EventSystem.Publish(new UnitHealedEvent(unit, target, processedAttack));
            await Task.Delay(ScaledAttackDelay);
        }
        
        public ProcessedAttack RunHeal(Unit attacker, Unit defender)
        {
            var attack = CalculateAttack(attacker);
            defender.ApplyHealingToTarget((int)attack.Damages.First().Value);
            return new ProcessedAttack(attack.Damages, Array.Empty<Damage>());
        }
    }
}