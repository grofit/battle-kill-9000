using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using OpenRpg.Core.Effects;
using OpenRpg.Genres.Fantasy.Types;

namespace BK9K.Game.Extensions
{
    public static class EffectExtensions
    {
        public static readonly HashSet<int> AllPercentageTypes = GetAllPercentageEffectTypes();

        public static HashSet<int> GetAllPercentageEffectTypes()
        {
            var percentageTypes = typeof(EffectTypes)
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(x => x.Name.Contains("Percentage"))
                .Select(x => (int)x.GetValue(null));

            return new HashSet<int>(percentageTypes);
        }

        public static bool IsPercentageEffect(this Effect effect)
        { return AllPercentageTypes.Contains(effect.EffectType); }

        public static bool IsPercentageEffect(this int effectType)
        { return AllPercentageTypes.Contains(effectType); }
    }
}