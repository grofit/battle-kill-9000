using BK9K.Framework.Units;
using OpenRpg.Genres.Fantasy.Extensions;

namespace BK9K.Framework.Extensions
{
    public static class UnitExtensions
    {
        public static bool IsDead(this Unit unit)
        { return unit.Stats.Health() == 0; }
    }
}