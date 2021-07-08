using System;

namespace BK9K.Mechanics.Abilities
{
    public readonly struct AbilityShape : IEquatable<AbilityShape>
    {
        /// <summary>
        /// This indicates the source target point, i.e where a spell would be cast
        /// </summary>
        public static byte TargetPoint = 1;
        
        /// <summary>
        /// This indicates an empty cell in the shape
        /// </summary>
        public static byte EmptyPoint = 0;

        public byte Size { get; }
        public byte FacingDirection { get; }
        public byte[,] ShapeData { get; }
        
        public byte this[int y, int x] => ShapeData[y, x];
        
        /// <summary>
        /// All shapes are assumed to be facing upwards by default
        /// </summary>
        /// <param name="size">Size of the shape i.e 3 indicates 3x3</param>
        /// <param name="shapeData">The actual data for the shape</param>
        /// <param name="facingDirection">The direction the shape is facing</param>
        public AbilityShape(byte size, byte[,] shapeData, byte facingDirection = 1)
        {
            if(shapeData.Length != size*size)
            { throw new Exception($"Expected {size}x{size} shape but data did not match"); }
            
            ShapeData = shapeData;
            FacingDirection = facingDirection;
            Size = size;
        }

        public bool Equals(AbilityShape other)
        {
            return Size == other.Size && FacingDirection == other.FacingDirection && Equals(ShapeData, other.ShapeData);
        }

        public override bool Equals(object obj)
        {
            return obj is AbilityShape other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Size, FacingDirection, ShapeData);
        }
    }
}