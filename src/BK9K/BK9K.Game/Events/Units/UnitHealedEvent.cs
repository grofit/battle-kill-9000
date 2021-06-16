using BK9K.Mechanics.Units;
using OpenRpg.Combat.Processors;

namespace BK9K.Game.Events.Units
{
    public class UnitHealedEvent
    {
        public Unit Healer { get; set; }
        public Unit Target { get; set; }
        public ProcessedAttack ProcessedAttack { get; set; }

        public UnitHealedEvent(Unit healer, Unit target, ProcessedAttack processedAttack)
        {
            Healer = healer;
            Target = target;
            ProcessedAttack = processedAttack;
        }
    }
}