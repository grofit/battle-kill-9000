﻿using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using BK9K.Game.Levels;
using BK9K.Game.Units;
using BK9K.Mechanics.Extensions;
using BK9K.Mechanics.Types;
using BK9K.Mechanics.Units;

namespace BK9K.Game.Extensions
{
    public static class LevelExtensions
    {
        public static GameUnit GetUnitAt(this Level level, Vector2 position)
        { return level.GameUnits?.SingleOrDefault(x => x.Unit.Position == position); }
        
        public static IEnumerable<GameUnit> GetAliveUnits(this Level level)
        { return level.GameUnits.Where(x => !x.Unit.IsDead()); }

        public static IEnumerable<GameUnit> GetAllUnitsInFaction(this Level level, int factionType)
        { return level.GameUnits.Where(x => x.Unit.FactionType == factionType);}
            
        public static IEnumerable<GameUnit> GetAllEnemies(this Level level)
        { return GetAllUnitsInFaction(level, FactionTypes.Enemy); }
    }
}