using System.Collections.Generic;
using System.Linq;
using BK9K.Framework.Levels;
using BK9K.Framework.Transforms;
using BK9K.Framework.Units;

namespace BK9K.Framework.Extensions
{
    public static class LevelExtensions
    {
        public static Unit GetUnitAt(this Level level, Position position)
        { return level.Units?.SingleOrDefault(x => x.Position == position); }
        
        public static IEnumerable<Unit> GetAliveUnits(this Level level)
        { return level.Units.Where(x => !x.IsDead()); }
    }
}