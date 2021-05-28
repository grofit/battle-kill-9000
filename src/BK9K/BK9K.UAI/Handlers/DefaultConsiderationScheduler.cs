using System;
using System.Reactive;
using System.Reactive.Linq;

namespace BK9K.UAI.Handlers
{
    public class DefaultConsiderationScheduler : IConsiderationScheduler
    {
        public IObservable<Unit> DefaultRefreshPeriod { get; } =  Observable.Timer(TimeSpan.FromSeconds(0.5)).Select(x => Unit.Default);
    }
}