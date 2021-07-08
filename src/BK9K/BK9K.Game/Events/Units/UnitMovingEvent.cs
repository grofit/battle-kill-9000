using System.Numerics;
using BK9K.Mechanics.Units;

namespace BK9K.Game.Events.Units
{
    public class UnitMovingEvent
    {
        public Vector2 OldPosition { get; }
        public int OldDirection { get; }
        public Unit Unit { get; }

        public UnitMovingEvent(Unit unit, Vector2 oldPosition, int oldDirection)
        {
            Unit = unit;
            OldPosition = oldPosition;
            OldDirection = oldDirection;
        }
    }
}