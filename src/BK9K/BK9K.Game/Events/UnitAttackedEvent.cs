using BK9K.Framework.Units;

namespace BK9K.Game.Events
{
    public class UnitAttackedEvent
    {
        public Unit Attacker { get; set; }
        public Unit Target { get; set; }
        public int Damage { get; set; }

        public UnitAttackedEvent(Unit attacker, Unit target, int damage = 0)
        {
            Attacker = attacker;
            Target = target;
            Damage = damage;
        }
    }
}