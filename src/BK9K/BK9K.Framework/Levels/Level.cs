using System.Collections.Generic;
using BK9K.Framework.Grids;
using BK9K.Framework.Units;

namespace BK9K.Framework.Levels
{
    public class Level
    {
        public Grid Grid { get; set; } = new Grid();
        public List<Unit> Units { get; set; } = new List<Unit>();
        public bool HasLevelFinished { get; set; } = false;
    }
}