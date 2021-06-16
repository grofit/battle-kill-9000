using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using BK9K.Game.Extensions;
using BK9K.Game.Levels;
using BK9K.Mechanics.Units;

namespace BK9K.Game.Movement
{
    public class MovementAdvisor
    {
        public Level Level { get; }

        public MovementAdvisor(Level level)
        { Level = level; }

        public Vector2 GetBestMovement(Unit unit, Unit target)
        {
            return GetPossibleMovementLocationsFor(unit)
                .OrderBy(x => Vector2.Distance(target.Position, unit.Position))
                .FirstOrDefault();
        }
        
        public IEnumerable<Vector2> GetPossibleMovementLocationsFor(Unit unit)
        {
            var minX = unit.Position.X - unit.MovementRange;
            var maxX = unit.Position.X + unit.MovementRange;
            var minY = unit.Position.Y - unit.MovementRange;
            var maxY = unit.Position.Y + unit.MovementRange;
           
            if (minX < 0) { minX = 0; }
            if (maxX > Level.Grid.XSize-1) { minX = Level.Grid.XSize-1; }
            if (minY < 0) { minY = 0; }
            if (maxY < Level.Grid.YSize-1) { maxY = Level.Grid.YSize-1; }

            for (var x = minX; x <= maxX; x++)
            {
                for (var y = minY; y <= maxY; y++)
                {
                    var proposedPosition = new Vector2(x, y);
                    if (IsPositionValid(proposedPosition))
                    { yield return proposedPosition; }
                }
            }
        }

        public bool IsPositionValid(Vector2 position)
        {
            var possibleUnit = Level.GetUnitAt(position);
            return possibleUnit == null;
        }
    }
}