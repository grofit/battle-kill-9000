using BK9K.Mechanics.Units;
using BK9K.UAI;

namespace BK9K.Game.Extensions
{
    public static class IAgentExtensions
    {
        public static Unit GetRelatedUnit(this IAgent agent)
        { return agent.RelatedContext as Unit; }
    }
}