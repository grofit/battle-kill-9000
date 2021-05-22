using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using BK9K.Mechanics.Levels;
using BK9K.Mechanics.Units;

namespace BK9K.Mechanics.Extensions
{
    public static class LevelExtensions
    {
        public static Unit GetUnitAt(this Level level, Vector2 position)
        { return level.Units?.SingleOrDefault(x => x.Position == position); }
        
        public static IEnumerable<Unit> GetAliveUnits(this Level level)
        { return level.Units.Where(x => !x.IsDead()); }
    }
}