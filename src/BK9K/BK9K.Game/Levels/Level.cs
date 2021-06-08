using System.Collections.Generic;
using BK9K.Game.Units;
using BK9K.Mechanics.Grids;
using OpenRpg.Core.Common;

namespace BK9K.Game.Levels
{
    public class Level : IHasDataId
    {
        public int Id { get; set; }
        public Grid Grid { get; set; } = new();
        public List<GameUnit> GameUnits { get; set; } = new();
        public bool HasLevelFinished { get; set; }
        public bool IsLevelLoading { get; set; }
    }
}