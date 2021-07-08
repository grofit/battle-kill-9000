using System;
using System.Collections.Generic;
using System.Numerics;
using BK9K.Mechanics.Abilities;
using BK9K.Mechanics.Types;

namespace BK9K.Mechanics.Extensions
{
    public static class Vector2Extensions
    {
        public static IEnumerable<Vector2> GetLocationsInRange(this Vector2 position, float range)
        {
            var minX = position.X - range;
            var maxX = position.X + range;
            var minY = position.Y - range;
            var maxY = position.Y + range;

            for (var x = minX; x <= maxX; x++)
            {
                for (var y = minY; y <= maxY; y++)
                {
                    yield return new Vector2(x, y);
                }
            }
        }

        public static IEnumerable<Vector2> GetLocationsInRange(this Vector2 position, float range, float xBounds, float yBounds)
        {
            var minX = position.X - range;
            var maxX = position.X + range;
            var minY = position.Y - range;
            var maxY = position.Y + range;

            if(minX < 0) { minX = 0; }
            if(maxX >= xBounds) { maxX = xBounds; }
            if(minY < 0) { minY = 0; }
            if(maxY >= yBounds) { maxY = yBounds; }
            
            for (var x = minX; x <= maxX; x++)
            {
                for (var y = minY; y <= maxY; y++)
                {
                    yield return new Vector2(x, y);
                }
            }
        }

        public static IEnumerable<Vector2> GetLocationsInLine(this Vector2 position, float rangeEitherSide, bool useXAxis = true)
        {
            for (var i = 0; i < rangeEitherSide; i++)
            {
                yield return new Vector2(position.X + (useXAxis ? 1 : 0), position.Y + (useXAxis ? 0 : 1));
                yield return new Vector2(position.X - (useXAxis ? 1 : 0), position.Y - (useXAxis ? 0 : 1));
            }
        }
        
        public static IEnumerable<Vector2> GetLocationsFromShape(this Vector2 position, AbilityShape shape)
        {
            var relativePositions = shape.GetPositionsRelativeToTarget();

            foreach (var relativePosition in relativePositions)
            { yield return position + relativePosition; }
        }

        public static int GetDirection(this Vector2 directionVector)
        {
            var absX = Math.Abs(directionVector.X);
            var absY = Math.Abs(directionVector.Y);

            if (absX > absY)
            { return directionVector.X > 0 ? DirectionTypes.Right : DirectionTypes.Left; }
            return directionVector.Y > 0 ? DirectionTypes.Up : DirectionTypes.Down;
        }
        
        public static bool isUnitAboveOrBelow(this Vector2 position, Vector2 targetPosition)
        {
            return position.X == targetPosition.X &&
                   (targetPosition.Y == position.Y + 1 || targetPosition.Y == position.Y - 1);
        }
    }
}