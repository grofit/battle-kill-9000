using BK9K.Mechanics.Units;
using OpenRpg.AdviceEngine;

namespace BK9K.Game.Extensions
{
    public static class IAgentExtensions
    {
        public static Unit GetOwnerUnit(this IAgent agent)
        { return agent.OwnerContext as Unit; }
    }
}