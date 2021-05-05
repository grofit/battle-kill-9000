using System.Collections.Generic;
using System.Linq;
using BK9K.Framework.Grids;
using BK9K.Framework.Transforms;
using BK9K.Framework.Units;

namespace BK9K.Game
{
    public class World
    {
        public Grid Grid { get; set; } = new Grid();
        public List<Unit> Units { get; set; } = new List<Unit>();
        
        public Unit GetUnitAt(Position position)
        { return Units?.SingleOrDefault(x => x.Position == position); }
    }
}