using System;
using System.Collections.Generic;

namespace BK9K.Framework.Transforms
{
    public struct Position
    {
        public int X, Y;

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static bool operator ==(Position p1, Position p2)
        { return p1.Equals(p2); }

        public static bool operator !=(Position p1, Position p2)
        { return !p1.Equals(p2); }

        public bool Equals(Position other)
        { return X == other.X && Y == other.Y; }

        public override bool Equals(object obj)
        { return obj is Position other && Equals(other); }

        public override int GetHashCode()
        { return HashCode.Combine(X, Y); }

        public static Position Zero => new Position();
    }
}