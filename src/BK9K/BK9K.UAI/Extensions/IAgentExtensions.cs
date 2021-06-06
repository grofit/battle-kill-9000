using System;
using System.Reactive;
using BK9K.UAI.Considerations;
using BK9K.UAI.Keys;

namespace BK9K.UAI.Extensions
{
    public static class IAgentExtensions
    {
        public static void AddConsideration(this IAgent agent, IConsideration consideration, IObservable<Unit> explicitUpdateTrigger = null)
        { agent.ConsiderationHandler.AddConsideration(consideration, explicitUpdateTrigger); }
 
        public static void RemoveConsideration(this IAgent agent, int utilityId)
        { agent.ConsiderationHandler.RemoveConsideration(new UtilityKey(utilityId)); }
        
        public static void RemoveConsideration(this IAgent agent, UtilityKey utilityKey)
        { agent.ConsiderationHandler.RemoveConsideration(utilityKey); }
        
        public static void RemoveConsideration(this IAgent agent, IConsideration consideration)
        { agent.ConsiderationHandler.RemoveConsideration(consideration); }
    }
}