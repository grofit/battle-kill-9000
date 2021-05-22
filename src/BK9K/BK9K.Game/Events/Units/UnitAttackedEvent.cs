using BK9K.Mechanics.Units;
using OpenRpg.Combat.Processors;

namespace BK9K.Game.Events.Units
{
    public class UnitAttackedEvent
    {
        public Unit Attacker { get; set; }
        public Unit Target { get; set; }
        public ProcessedAttack ProcessedAttack { get; set; }

        public UnitAttackedEvent(Unit attacker, Unit target, ProcessedAttack processedAttack)
        {
            Attacker = attacker;
            Target = target;
            ProcessedAttack = processedAttack;
        }
    }
}