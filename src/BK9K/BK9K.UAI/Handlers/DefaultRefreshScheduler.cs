using System;
using System.Reactive;
using System.Reactive.Linq;

namespace BK9K.UAI.Handlers.Considerations
{
    public class DefaultRefreshScheduler : IRefreshScheduler
    {
        public IObservable<Unit> DefaultRefreshPeriod { get; } =  Observable.Timer(TimeSpan.FromSeconds(0.5)).Select(x => Unit.Default);
    }
}