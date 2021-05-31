using System;
using SystemsRx.Events;
using BK9K.Game.Scheduler;
using BK9K.Mechanics.Units;
using BK9K.UAI;
using BK9K.UAI.Handlers;

namespace BK9K.Game.AI
{
    public class AgentBuilder
    {
        public IEventSystem EventSystem { get; }
        
        private Unit _relatedUnit;

        public AgentBuilder(IEventSystem eventSystem)
        {
            EventSystem = eventSystem;
        }

        public AgentBuilder Create()
        { return new(EventSystem); }

        public AgentBuilder ForUnit(Unit relatedUnit)
        {
            _relatedUnit = relatedUnit;
            return this;
        }

        public IAgent Build()
        {
            if (_relatedUnit == null)
            { throw new Exception("AgentBuilder requires a unit to build for"); }

            if (_relatedUnit.Id == 0)
            { throw new Exception("Unit must have a valid Id"); }
            
            var agent = new Agent(_relatedUnit, new ConsiderationHandler(new CustomConsiderationScheduler(EventSystem)));
            return agent;
        }
    }
}