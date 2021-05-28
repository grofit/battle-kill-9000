using System;
using System.Reactive;
using BK9K.UAI.Considerations;
using BK9K.UAI.Keys;

namespace BK9K.UAI.Extensions
{
    public static class IAgentExtensions
    {
        public static void AddConsideration(this IAgent agent, int utilityId, IConsideration consideration, IObservable<Unit> explicitUpdateTrigger = null)
        { agent.ConsiderationHandler.AddConsideration(new UtilityKey(utilityId), consideration, explicitUpdateTrigger); }
        
        public static void AddConsideration(this IAgent agent, UtilityKey utilityKey, IConsideration consideration, IObservable<Unit> explicitUpdateTrigger = null)
        { agent.ConsiderationHandler.AddConsideration(utilityKey, consideration, explicitUpdateTrigger); }
        
        public static void RemoveConsideration(this IAgent agent, int utilityId)
        { agent.ConsiderationHandler.RemoveConsideration(new UtilityKey(utilityId)); }
        
        public static void RemoveConsideration(this IAgent agent, UtilityKey utilityKey)
        { agent.ConsiderationHandler.RemoveConsideration(utilityKey); }
    }
}