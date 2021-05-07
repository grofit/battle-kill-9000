using BK9K.Framework.Units;

namespace BK9K.Game.Events.Units
{
    public class UnitEndTurn
    {
        public Unit Unit { get; }

        public UnitEndTurn(Unit unit)
        {
            Unit = unit;
        }
    }
}