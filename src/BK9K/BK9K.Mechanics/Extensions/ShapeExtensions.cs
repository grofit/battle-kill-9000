using System.Collections.Generic;
using System.Numerics;
using BK9K.Mechanics.Abilities;
using BK9K.Mechanics.Types;

namespace BK9K.Mechanics.Extensions
{
    public static class ShapeExtensions
    {
        public static AbilityShape Rotate90(this AbilityShape shape)
        {
            var result = new byte[shape.Size, shape.Size];

            for (var y = 0; y < shape.Size; ++y) 
            {
                for (var x = 0; x < shape.Size; ++x) 
                { result[y, x] = shape.ShapeData[shape.Size - x - 1, y]; }
            }

            var newDirection = (byte)(shape.FacingDirection + 1);
            if(newDirection > DirectionTypes.Left)
            { newDirection = DirectionTypes.Up; }
            return new AbilityShape(shape.Size, result, newDirection);
        }
        
        public static AbilityShape Rotate180(this AbilityShape shape)
        { return Rotate90(Rotate90(shape)); }
        
        public static AbilityShape RotateN90(this AbilityShape shape)
        {
            var result = new byte[shape.Size, shape.Size];

            for (var y = 0; y < shape.Size; ++y) 
            {
                for (var x = 0; x < shape.Size; ++x) 
                { result[y, x] = shape.ShapeData[x, shape.Size - y - 1]; }
            }
            
            var newDirection = (byte)(shape.FacingDirection - 1);
            if(newDirection < DirectionTypes.Up)
            { newDirection = DirectionTypes.Left; }
            return new AbilityShape(shape.Size, result, newDirection);
        }

        public static int ActiveCellCount(this AbilityShape shape)
        {
            var count = 0;
            for (var y = 0; y < shape.Size; ++y) 
            {
                for (var x = 0; x < shape.Size; ++x) 
                {
                    if(shape[y,x] != AbilityShape.EmptyPoint)
                    { count++;}
                }
            }
            return count;
        }

        public static IEnumerable<Vector2> GetPositionsRelativeToTarget(this AbilityShape shape)
        {
            var positions = new List<Vector2>();
            var targetPosition = Vector2.Zero;
            for (var y = 0; y < shape.Size; ++y) 
            {
                for (var x = 0; x < shape.Size; ++x) 
                {
                    if (shape[y, x] != AbilityShape.EmptyPoint)
                    {
                        if (shape[y, x] == AbilityShape.TargetPoint)
                        { targetPosition = new Vector2(x, y); }
                        else
                        { positions.Add(new Vector2(x,y)); }
                    }
                }
            }

            foreach (var position in positions)
            { yield return new Vector2(position.X - targetPosition.X, position.Y - targetPosition.Y); }
            yield return Vector2.Zero;
        }
    }
}