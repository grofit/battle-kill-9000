using System;
using System.Reactive;
using System.Reactive.Linq;
using SystemsRx.Events;
using BK9K.Game.Events.Units;
using BK9K.UAI.Handlers;

namespace BK9K.Game.Scheduler
{
    public class CustomConsiderationScheduler : IConsiderationScheduler
    {
        public IEventSystem EventSystem { get; }
        
        public IObservable<Unit> DefaultRefreshPeriod { get; }

        public CustomConsiderationScheduler(IEventSystem eventSystem)
        {
            EventSystem = eventSystem;
            DefaultRefreshPeriod = EventSystem.Receive<UnitEndTurn>().Select(x => Unit.Default);
        }
    }
}