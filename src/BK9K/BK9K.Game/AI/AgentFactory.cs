using System;
using SystemsRx.Events;
using BK9K.Game.Scheduler;
using BK9K.Mechanics.Units;
using OpenRpg.AdviceEngine;
using OpenRpg.AdviceEngine.Handlers.Advisors;
using OpenRpg.AdviceEngine.Handlers.Considerations;

namespace BK9K.Game.AI
{
    public class AgentFactory
    {
        public IEventSystem EventSystem { get; }
        
        public AgentFactory(IEventSystem eventSystem)
        {
            EventSystem = eventSystem;
        }
        
        public IAgent CreateFor(Unit unit)
        {
            if (unit == null)
            { throw new Exception("AgentBuilder requires a unit to build for"); }

            if (unit.Id == 0)
            { throw new Exception("Unit must have a valid Id"); }

            var considerationHandler = new ConsiderationHandler(new CustomRefreshScheduler(EventSystem));
            var adviceHandler = new AdviceHandler(new CustomRefreshScheduler(EventSystem));
            var agent = new Agent(unit, considerationHandler, adviceHandler);
            return agent;
        }
    }
}