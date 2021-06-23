using BK9K.Mechanics.Units;

namespace BK9K.Game.Events.Units
{
    public class UnitLeveledUpEvent
    {
        public Unit Unit { get; }

        public UnitLeveledUpEvent(Unit unit)
        {
            Unit = unit;
        }
    }
}