using System.Collections.Generic;
using System.Linq;
using OpenRpg.Combat.Effects;

namespace BK9K.Framework.Extensions
{
    public static class ActiveEffectExtensions
    {
        public static float SumPotencyFor(this IEnumerable<ActiveEffect> activeEffects, int effectType)
        {
            return activeEffects
                .Where(x => x.Effect.EffectType == effectType)
                .Sum(x => x.Effect.Potency);
        }

        public static int TicksSoFar(this ActiveEffect activeEffect)
        { return (int)(activeEffect.ActiveTime / activeEffect.Effect.Frequency); }
    }
}