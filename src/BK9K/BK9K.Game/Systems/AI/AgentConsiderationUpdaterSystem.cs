using System.Linq;
using SystemsRx.Systems.Conventional;
using BK9K.Game.Data.Variables;
using BK9K.Game.Events.Units;
using BK9K.Game.Levels;
using BK9K.UAI.Keys;

namespace BK9K.Game.Systems.AI
{
    public class AgentConsiderationUpdateSystem : IReactToEventSystem<UnitHasDiedEvent>
    {
        public Level Level { get; }

        public static int[] AssociatedUnitUtilityIds = {
            UtilityVariableTypes.EnemyDistance,
            UtilityVariableTypes.EnemyLowHealth,
            UtilityVariableTypes.PartyLowHealth,
            UtilityVariableTypes.IsADanger
        };
        
        public AgentConsiderationUpdateSystem(Level level)
        {
            Level = level;
        }

        public void Process(UnitHasDiedEvent eventData)
        {
            var unitId = eventData.Target.Id;
            RemoveUnitConsiderationsFor(unitId);
        }

        public void RemoveUnitConsiderationsFor(int unitId)
        {
            foreach (var agent in Level.GameUnits.Select(x => x.Agent))
            {
                foreach (var utilityId in AssociatedUnitUtilityIds)
                {
                    var utilityKey = new UtilityKey(utilityId, unitId);
                    if(agent.UtilityVariables.ContainsKey(utilityKey))
                    { agent.ConsiderationHandler.RemoveConsideration(utilityKey); }
                }
            }
        }
    }
}