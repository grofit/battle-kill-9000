using OpenRpg.Combat.Abilities;

namespace BK9K.Game.AI.Applicators.Models
{
    public class AbilityWithTarget
    {
        public int TargetUnitId { get; }
        public int AbilityId { get; }

        public AbilityWithTarget(int targetUnitId, int abilityId)
        {
            TargetUnitId = targetUnitId;
            AbilityId = abilityId;
        }
    }
}