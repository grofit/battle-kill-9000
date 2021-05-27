using System;
using System.Reactive;
using BK9K.UAI.Considerations;

namespace BK9K.UAI.Extensions
{
    public static class IAgentExtensions
    {
        public static void AddConsideration(this IAgent agent, int utilityId, IConsideration consideration, IObservable<Unit> explicitUpdateTrigger = null)
        { agent.ConsiderationHandler.AddConsideration(utilityId, consideration, explicitUpdateTrigger); }
        
        public static void RemoveConsideration(this IAgent agent, int utilityId)
        { agent.ConsiderationHandler.RemoveConsideration(utilityId); }
    }
}