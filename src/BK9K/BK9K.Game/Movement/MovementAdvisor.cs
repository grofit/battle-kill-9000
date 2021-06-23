using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using BK9K.Game.Extensions;
using BK9K.Game.Levels;
using BK9K.Mechanics.Extensions;
using BK9K.Mechanics.Units;

namespace BK9K.Game.Movement
{
    public class MovementAdvisor
    {
        public Level Level { get; }

        public MovementAdvisor(Level level)
        { Level = level; }

        public Vector2 GetBestMovementTowardsTarget(Unit unit, Unit target)
        {
            return GetPossibleMovementLocationsFor(unit)
                .OrderBy(x => Vector2.Distance(x, target.Position))
                .FirstOrDefault();
        }

        public Vector2 GetBestMovementAwayFromTarget(Unit unit, Unit target)
        { return GetBestMovementAwayFromLocation(unit, target.Position); }

        public Vector2 GetBestMovementAwayFromLocation(Unit unit, Vector2 position)
        {
            return GetPossibleMovementLocationsFor(unit)
                .OrderByDescending(x => Vector2.Distance(x, position))
                .FirstOrDefault();
        }

        public IEnumerable<Vector2> GetPossibleMovementLocationsFor(Unit unit)
        {
            return unit.Position
                .GetLocationsInRange(unit.MovementRange, Level.Grid.XSize - 1, Level.Grid.YSize - 1)
                .Where(IsPositionValid);
        }

        public bool IsPositionValid(Vector2 position)
        {
            var possibleUnit = Level.GetUnitAt(position);
            return possibleUnit == null;
        }
    }
}