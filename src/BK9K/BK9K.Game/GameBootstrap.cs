using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using BK9K.Framework.Extensions;
using BK9K.Framework.Grids;
using BK9K.Framework.Transforms;
using BK9K.Framework.Types;
using BK9K.Framework.Units;

namespace BK9K.Game
{
    public class GameBootstrap : IDisposable
    {
        public Grid Grid { get; set; }
        public List<Unit> Units { get; set; } = new List<Unit>();

        public IObservable<System.Reactive.Unit> OnUpdated => _onUpdated;
        private readonly Subject<System.Reactive.Unit> _onUpdated = new();

        private IDisposable _gameLoopSub;

        public Unit GetUnitAt(Position position)
        { return Units.SingleOrDefault(x => x.Position == position); }

        public void StartGame()
        {
            Grid = GridBuilder.Create()
                .WithSize(5, 5)
                .Build();

            var playerUnit = UnitBuilder.Create()
                .WithName("Gooch")
                .WithFaction(FactionTypes.Player)
                .WithClass(ClassTypes.Fighter)
                .WithAttack(6)
                .WithInitiative(6)
                .WithPosition(3, 2)
                .Build();

            var enemyUnit1 = UnitBuilder.Create()
                .WithName("Enemy Person 1")
                .WithInitiative(3)
                .WithAttack(3)
                .WithClass(ClassTypes.Mage)
                .WithPosition(3, 3)
                .Build();

            var enemyUnit2 = UnitBuilder.Create()
                .WithName("Enemy Person 2")
                .WithInitiative(2)
                .WithAttack(2)
                .WithClass(ClassTypes.Rogue)
                .WithPosition(3, 1)
                .Build();

            Units.Add(playerUnit);
            Units.Add(enemyUnit1);
            Units.Add(enemyUnit2);

            _gameLoopSub = Observable.Interval(TimeSpan.FromSeconds(1)).Subscribe(Update);
        }

        protected void Update(long elapsed)
        {
            /*
            PlayerAttack();
            if (!EnemyUnit.IsDead())
            { EnemyAttack(); }
            */
            _onUpdated.OnNext(System.Reactive.Unit.Default);
        }
        
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