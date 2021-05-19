using System;
using System.Linq;
using BK9K.Framework.Units;
using OpenRpg.Combat.Processors;
using OpenRpg.Genres.Fantasy.Extensions;

namespace BK9K.Game.Extensions
{
    public static class UnitExtensions
    {
        public static void ApplyDamageToTarget(this Unit target, ProcessedAttack attack)
        {
            var summedAttack = attack.DamageDone.Sum(x => x.Value);
            var totalDamage = (int)Math.Round(summedAttack);

            if (target.Stats.Health() >= totalDamage)
            { target.Stats.Health(target.Stats.Health() - totalDamage); }
            else
            { target.Stats.Health(0); }
        }
    }
}