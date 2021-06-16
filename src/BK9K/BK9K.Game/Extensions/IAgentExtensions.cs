using BK9K.Mechanics.Units;
using OpenRpg.AdviceEngine;

namespace BK9K.Game.Extensions
{
    public static class IAgentExtensions
    {
        public static Unit GetRelatedUnit(this IAgent agent)
        { return agent.RelatedContext as Unit; }
    }
}