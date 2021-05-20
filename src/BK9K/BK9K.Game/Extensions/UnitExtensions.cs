using System;
using System.Linq;
using BK9K.Framework.Units;
using OpenRpg.Combat.Processors;
using OpenRpg.Genres.Fantasy.Extensions;
using OpenRpg.Genres.Fantasy.Types;

namespace BK9K.Game.Extensions
{
    public static class UnitExtensions
    {
        public static void ApplyDamageToTarget(this Unit target, ProcessedAttack attack)
        {
            var summedAttack = attack.DamageDone.Sum(x => x.Value);
            var totalDamage = (int)Math.Round(summedAttack);
            if(totalDamage < 0) { totalDamage = 0; }
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