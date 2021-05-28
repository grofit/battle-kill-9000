using System.Collections.Generic;
using BK9K.Game.Units;
using BK9K.Mechanics.Grids;
using BK9K.Mechanics.Units;

namespace BK9K.Game.Levels
{
    public class Level
    {
        public Grid Grid { get; set; } = new();
        public List<GameUnit> GameUnits { get; set; } = new();
        public bool HasLevelFinished { get; set; }
    }
}