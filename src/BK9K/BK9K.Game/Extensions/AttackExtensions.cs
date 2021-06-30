using System.Collections.Generic;
using OpenRpg.Combat.Attacks;

namespace BK9K.Game.Extensions
{
    public static class AttackExtensions
    {
        public static Attack ScaleByPercentage(this Attack attack, float percentage)
        {
            var newDamages = new List<Damage>();
            foreach (var damage in attack.Damages)
            {
                var newDamage = damage.Value * percentage;
                newDamages.Add(new Damage(damage.Type, newDamage));
            }
            return new Attack(newDamages);
        }
    }
}