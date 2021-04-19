using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using BK9K.Framework.Extensions;
using BK9K.Framework.Grids;
using BK9K.Framework.Units;

namespace BK9K.Web
{
    public class Game : IDisposable
    {
        public Grid Grid { get; set; }
        public Unit PlayerUnit { get; set; }
        public Unit EnemyUnit { get; set; }

        public IObservable<System.Reactive.Unit> OnUpdated => _onUpdated;
        private readonly Subject<System.Reactive.Unit> _onUpdated = new Subject<System.Reactive.Unit>();

        private IDisposable _gameLoopSub;
        
        public void StartGame()
        {
            Grid = GridBuilder.Create()
                .WithSize(5, 5)
                .Build();

            PlayerUnit = UnitBuilder.Create()
                .WithName("Gooch")
                .Build();

            EnemyUnit = UnitBuilder.Create()
                .WithName("Enemy Person")
                .WithAttack(5)
                .Build();

            _gameLoopSub = Observable.Interval(TimeSpan.FromSeconds(1)).Subscribe(Update);
        }

        protected void Update(long elapsed)
        {
            PlayerAttack();
            if (!EnemyUnit.IsDead())
            { EnemyAttack(); }

            _onUpdated.OnNext(System.Reactive.Unit.Default);
        }
        
        public void PlayerAttack() => RunAttack(PlayerUnit, EnemyUnit);
        public void EnemyAttack() => RunAttack(EnemyUnit, PlayerUnit);

        public void RunAttack(Unit attacker, Unit defender)
        {
            if (defender.Health >= attacker.Attack)
            { defender.Health -= attacker.Attack; }
            else
            { defender.Health = 0; }
        }

        public void Dispose()
        {
            _gameLoopSub?.Dispose();
            _onUpdated?.Dispose();
        }
    }
}