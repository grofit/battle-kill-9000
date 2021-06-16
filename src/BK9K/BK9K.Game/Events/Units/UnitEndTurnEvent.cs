using BK9K.Mechanics.Units;

namespace BK9K.Game.Events.Units
{
    public class UnitEndTurnEvent
    {
        public Unit Unit { get; }

        public UnitEndTurnEvent(Unit unit)
        {
            Unit = unit;
        }
    }
}