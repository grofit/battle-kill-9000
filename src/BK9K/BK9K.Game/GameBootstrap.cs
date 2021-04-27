using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace BK9K.Game
{
    public class GameBootstrap : IDisposable
    {
        public IObservable<System.Reactive.Unit> OnStarted => _onStarted;
        private readonly Subject<System.Reactive.Unit> _onStarted = new();

        public IObservable<System.Reactive.Unit> OnStopped => _onStopped;
        private readonly Subject<System.Reactive.Unit> _onStopped = new();

        public IObservable<long> OnUpdated => _onUpdated;
        private readonly Subject<long> _onUpdated = new();
        
        private IDisposable _gameLoopSub;
        
        public void StartGame()
        {
            _gameLoopSub = Observable.Interval(TimeSpan.FromSeconds(1)).Subscribe(Update);
            _onStarted.OnNext(System.Reactive.Unit.Default);
        }
        
        protected void Update(long elapsed)
        {
            _onUpdated?.OnNext(elapsed);
        }

        public void Dispose()
        {
            _gameLoopSub?.Dispose();
            _onUpdated?.Dispose();

            _onStopped.OnNext(System.Reactive.Unit.Default);
            _onStarted?.Dispose();
            _onStopped?.Dispose();
        }
    }
}