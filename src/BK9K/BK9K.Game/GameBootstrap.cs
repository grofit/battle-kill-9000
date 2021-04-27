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
using BK9K.Game.Events;

namespace BK9K.Game
{
    public class GameBootstrap : IDisposable
    {
        public Grid Grid { get; set; }
        public List<Unit> Units { get; set; } = new List<Unit>();

        public IObservable<System.Reactive.Unit> OnUpdated => _onUpdated;
        private readonly Subject<System.Reactive.Unit> _onUpdated = new();

        public IObservable<UnitAttackedEvent> OnUnitAttacked => _onUnitAttacked;
        private readonly Subject<UnitAttackedEvent> _onUnitAttacked = new();

        public IObservable<GameResolvedEvent> OnGameResolved => _onGameResolved;
        private readonly Subject<GameResolvedEvent> _onGameResolved = new();

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
                .WithFaction(FactionTypes.Enemy)
                .WithAttack(2)
                .WithClass(ClassTypes.Mage)
                .WithPosition(3, 3)
                .Build();

            var enemyUnit2 = UnitBuilder.Create()
                .WithName("Enemy Person 2")
                .WithFaction(FactionTypes.Enemy)
                .WithInitiative(2)
                .WithAttack(1)
                .WithClass(ClassTypes.Rogue)
                .WithPosition(3, 1)
                .Build();

            var enemyUnit3 = UnitBuilder.Create()
                .WithName("Enemy Person 3")
                .WithFaction(FactionTypes.Enemy)
                .WithInitiative(2)
                .WithAttack(1)
                .WithClass(ClassTypes.Fighter)
                .WithPosition(2, 2)
                .Build();

            Units.Add(playerUnit);
            Units.Add(enemyUnit1);
            Units.Add(enemyUnit2);
            Units.Add(enemyUnit3);

            _gameLoopSub = Observable.Interval(TimeSpan.FromSeconds(1)).Subscribe(Update);
        }

        public bool HasPlayerWon()
        { return !Units.Any(x => x.FactionType == FactionTypes.Enemy && !x.IsDead()); }

        public bool HasPlayerLost()
        { return !Units.Any(x => x.FactionType == FactionTypes.Player && !x.IsDead()); }

        public void PlayRound()
        {
            Units.Where(x => !x.IsDead())
                .OrderBy(x => x.Initiative)
                .ToList()
                .ForEach(TakeTurn);
        }

        protected void Update(long elapsed)
        {
            if (!HasPlayerWon() && !HasPlayerLost())
            {
                PlayRound();
                
                if (HasPlayerWon()) { _onGameResolved.OnNext(new GameResolvedEvent(true)); }
                else if (HasPlayerLost()) { _onGameResolved.OnNext(new GameResolvedEvent(false)); }
            }

            _onUpdated?.OnNext(System.Reactive.Unit.Default);
        }

        public void TakeTurn(Unit unit)
        {
            return;
            var target = Units.FirstOrDefault(x => x.FactionType != unit.FactionType && !x.IsDead());
            var damage = RunAttack(unit, target);
            if (target.IsDead()) { unit.Level++; }
            _onUnitAttacked?.OnNext(new UnitAttackedEvent(unit, target, damage));
        }

        public byte GenerateAttack(Unit unit)
        { return (byte)(unit.Attack + ((unit.Attack / 5) * unit.Level)); }

        public int RunAttack(Unit attacker, Unit defender)
        {
            var damage = GenerateAttack(attacker);
            if (defender.Health >= damage)
            { defender.Health -= damage; }
            else
            { defender.Health = 0; }
            return damage;
        }

        public void Dispose()
        {
            _gameLoopSub?.Dispose();
            _onUpdated?.Dispose();
            _onUnitAttacked?.Dispose();
            _onGameResolved?.Dispose();
        }
    }
}