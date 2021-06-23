using BK9K.Mechanics.Units;

namespace BK9K.Game.Events.Units
{
    public class UnitHasDiedEvent
    {
        public Unit Target { get; set; }
        public Unit Killer { get; set; }

        public UnitHasDiedEvent(Unit target, Unit killer)
        {
            Target = target;
            Killer = killer;
        }
    }
}