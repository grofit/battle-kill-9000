﻿using System.Collections.Generic;
using BK9K.Mechanics.Grids;
using BK9K.Mechanics.Units;

namespace BK9K.Mechanics.Levels
{
    public class Level
    {
        public Grid Grid { get; set; } = new Grid();
        public List<Unit> Units { get; set; } = new List<Unit>();
        public bool HasLevelFinished { get; set; } = false;
    }
}