using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using BK9K.Game.Levels;
using BK9K.Game.Units;
using BK9K.Mechanics.Abilities;
using BK9K.Mechanics.Extensions;
using BK9K.Mechanics.Types;
using BK9K.Mechanics.Units;

namespace BK9K.Game.Extensions
{
    public static class LevelExtensions
    {
        public static GameUnit GetUnitAt(this Level level, Vector2 position)
        { return level.GameUnits?.SingleOrDefault(x => x.Unit.Position == position); }

        public static bool HasUnitAt(this Level level, Vector2 position)
        { return level.GameUnits?.Any(x => x.Unit.Position == position) ?? false; }

        public static IEnumerable<GameUnit> GetAliveUnits(this Level level)
        { return level.GameUnits.Where(x => !x.Unit.IsDead()); }

        public static IEnumerable<GameUnit> GetAllUnitsInFaction(this Level level, int factionType)
        { return level.GameUnits.Where(x => x.Unit.FactionType == factionType);}

        public static IEnumerable<Unit> GetAllUnitsInFaction(this IEnumerable<Unit> units, int factionType)
        { return units.Where(x => x.FactionType == factionType); }
        
        public static IEnumerable<Unit> ThatAreAlive(this IEnumerable<Unit> units)
        { return units.Where(x => !x.IsDead()); }
        
        public static IEnumerable<GameUnit> ThatAreAlive(this IEnumerable<GameUnit> units)
        { return units.Where(x => !x.Unit.IsDead()); }
            
        public static IEnumerable<GameUnit> GetAllEnemies(this Level level)
        { return GetAllUnitsInFaction(level, FactionTypes.Enemy); }

        public static int GetAdjacentUnitCountFor(this Level level, Unit unit, bool onlyCountSameFaction = true)
        {
            bool CheckSameFaction(Vector2 x)
            {
                var unitToCheck = level.GetUnitAt(x).Unit;
                if(unitToCheck == null) { return false; }
                return unitToCheck.FactionType == unit.FactionType && !unitToCheck.IsDead();
            }
            
            bool CheckAnyFaction(Vector2 x)
            { return !level.GetUnitAt(x).Unit?.IsDead() ?? false; }
            
            var surroundingLocations = unit.Position.GetLocationsInRange(1);
            return surroundingLocations.Count(onlyCountSameFaction ? CheckSameFaction : CheckAnyFaction);
        }
        
        public static Unit GetUnitById(this Level level, int unitId)
        { return level.GameUnits.FirstOrDefault(x => x.Agent.OwnerContext.Id == unitId)?.Unit; }

        public static IEnumerable<GameUnit> GetAllUnitsInShape(this Level level, Vector2 targetPosition, AbilityShape shape)
        {
            var targetPositions = targetPosition.GetLocationsFromShape(shape);
            foreach (var position in targetPositions)
            {
                var possibleUnit = GetUnitAt(level, position);
                if (possibleUnit != null)
                { yield return possibleUnit; }
            }
        }

        public static int CountUnitsInShape(this Level level, Vector2 targetPosition, AbilityShape shape,
            bool aliveOnly = true)
        {
            var allUnits = GetAllUnitsInShape(level, targetPosition, shape);
            return aliveOnly ? allUnits.ThatAreAlive().Count() : allUnits.Count();
        }
    }
}