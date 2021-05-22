using System;
using System.Collections.Generic;
using System.Linq;
using BK9K.Mechanics.Units;
using OpenRpg.Combat.Processors;
using OpenRpg.Core.Effects;
using OpenRpg.Genres.Fantasy.Extensions;
using OpenRpg.Genres.Fantasy.Types;

namespace BK9K.Mechanics.Extensions
{
    public static class UnitExtensions
    {
        public static bool IsDead(this Unit unit)
        { return unit.Stats.Health() == 0; }

        public static IEnumerable<Effect> GetUnitEffects(this Unit unit)
        {
            var unitEffects = new List<Effect>();
            unitEffects.AddRange(unit.GetActiveEffects());
            unitEffects.AddRange(unit.PassiveEffects);
            return unitEffects;
        }

        public static void ApplyDamageToTarget(this Unit target, ProcessedAttack attack)
        {
            var summedAttack = attack.DamageDone.Sum(x => x.Value);
            var totalDamage = (int)Math.Round(summedAttack);
            if (totalDamage < 0) { totalDamage = 0; }
            target.Stats.DeductHealth(totalDamage);
        }

        public static void ApplyHealingToTarget(this Unit target, int healAmount)
        {
            var healBonus = target.Stats.GetDefenseFromDamageType(DamageTypes.LightDamage);
            var totalHealing = (int)Math.Round(healAmount + healBonus);

            if (totalHealing < 0)
            {
                var invertHealing = Math.Abs(totalHealing);
                target.Stats.DeductHealth(invertHealing);
                return;
            }

            target.Stats.AddHealth(totalHealing);
        }
    }
}