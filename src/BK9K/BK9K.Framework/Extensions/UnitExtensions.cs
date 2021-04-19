using BK9K.Framework.Units;

namespace BK9K.Framework.Extensions
{
    public static class UnitExtensions
    {
        public static bool IsDead(this Unit unit)
        { return unit.Health == 0; }
    }
}