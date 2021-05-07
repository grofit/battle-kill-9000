using BK9K.Framework.Units;

namespace BK9K.Game.Events.Units
{
    public class UnitActionPhase
    {
        public Unit Unit { get; }

        public UnitActionPhase(Unit unit)
        {
            Unit = unit;
        }
    }
}