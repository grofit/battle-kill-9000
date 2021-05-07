using BK9K.Framework.Units;

namespace BK9K.Game.Events.Units
{
    public class UnitStartTurn
    {
        public Unit Unit { get; }

        public UnitStartTurn(Unit unit)
        {
            Unit = unit;
        }
    }
}