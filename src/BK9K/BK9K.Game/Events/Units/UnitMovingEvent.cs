using BK9K.Framework.Transforms;
using BK9K.Framework.Units;

namespace BK9K.Game.Events.Units
{
    public class UnitMovingEvent
    {
        public Position OldPosition { get; set; }
        public Unit Unit { get; }

        public UnitMovingEvent(Unit unit, Position oldPosition)
        {
            Unit = unit;
            OldPosition = oldPosition;
        }
    }
}