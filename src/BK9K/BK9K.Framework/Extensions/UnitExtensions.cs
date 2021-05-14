using System.Collections.Generic;
using BK9K.Framework.Units;
using OpenRpg.Core.Effects;
using OpenRpg.Genres.Fantasy.Extensions;

namespace BK9K.Framework.Extensions
{
    public static class UnitExtensions
    {
        public static bool IsDead(this Unit unit)
        { return unit.Stats.Health() == 0; }

        public static IEnumerable<Effect> GetUnitEffects(this Unit unit)
        {
            var unitEffects = new List<Effect>();
            unitEffects.AddRange(unit.GetActiveEffects());
            unitEffects.AddRange(unit.CardEffects);
            return unitEffects;
        }
    }
}