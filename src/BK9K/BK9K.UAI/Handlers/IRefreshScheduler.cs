using System;
using System.Reactive;

namespace BK9K.UAI.Handlers
{
    public interface IRefreshScheduler
    {
        IObservable<Unit> DefaultRefreshPeriod { get; }
    }
}