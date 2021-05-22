using System.Numerics;
using BK9K.Mechanics.Units;

namespace BK9K.Game.Events.Units
{
    public class UnitMovingEvent
    {
        public Vector2 OldPosition { get; set; }
        public Unit Unit { get; }

        public UnitMovingEvent(Unit unit, Vector2 oldPosition)
        {
            Unit = unit;
            OldPosition = oldPosition;
        }
    }
}