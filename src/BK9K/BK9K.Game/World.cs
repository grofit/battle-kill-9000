using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using BK9K.Framework.Extensions;
using BK9K.Framework.Grids;
using BK9K.Framework.Transforms;
using BK9K.Framework.Types;
using BK9K.Framework.Units;
using BK9K.Game.Events;
using OpenRpg.Core.Classes;
using OpenRpg.Genres.Fantasy.Extensions;
using UnitBuilder = BK9K.Game.Builders.UnitBuilder;

namespace BK9K.Game
{
    public class World : IDisposable
    {
        public UnitBuilder UnitBuilder { get; }

        public Grid Grid { get; set; }
        public List<Unit> Units { get; set; } = new List<Unit>();

        public IObservable<UnitAttackedEvent> OnUnitAttacked => _onUnitAttacked;
        private readonly Subject<UnitAttackedEvent> _onUnitAttacked = new();

        public IObservable<GameResolvedEvent> OnGameResolved => _onGameResolved;
        private readonly Subject<GameResolvedEvent> _onGameResolved = new();
        
        public World(UnitBuilder unitBuilder)
        {
            UnitBuilder = unitBuilder;

            SetupGrid();
            SetupUnits();
        }

        public Unit GetUnitAt(Position position)
        { return Units.SingleOrDefault(x => x.Position == position); }

        public void SetupGrid()
        {
            Grid = GridBuilder.Create()
                .WithSize(5, 5)
                .Build();
        }

        private void SetupUnits()
        {
            var playerUnit1 = UnitBuilder.Create()
                .WithName("Gooch")
                .WithFaction(FactionTypes.Player)
                .WithClass(ClassTypes.Fighter)
                .WithInitiative(6)
                .WithPosition(3, 2)
                .Build();

            var playerUnit2 = UnitBuilder.Create()
                .WithName("Kate")
                .WithFaction(FactionTypes.Player)
                .WithClass(ClassTypes.Fighter)
                .WithInitiative(6)
                .WithPosition(1, 1)
                .Build();

            var enemyUnit1 = UnitBuilder.Create()
                .WithName("Enemy Person 1")
                .WithInitiative(3)
                .WithFaction(FactionTypes.Enemy)
                .WithClass(ClassTypes.Mage)
                .WithPosition(3, 3)
                .Build();

            var enemyUnit2 = UnitBuilder.Create()
                .WithName("Enemy Person 2")
                .WithFaction(FactionTypes.Enemy)
                .WithInitiative(2)
                .WithClass(ClassTypes.Rogue)
                .WithPosition(3, 1)
                .Build();

            var enemyUnit3 = UnitBuilder.Create()
                .WithName("Enemy Person 3")
                .WithFaction(FactionTypes.Enemy)
                .WithInitiative(2)
                .WithClass(ClassTypes.Fighter)
                .WithPosition(2, 2)
                .Build();

            Units.Add(playerUnit1);
            Units.Add(playerUnit2);
            Units.Add(enemyUnit1);
            Units.Add(enemyUnit2);
            Units.Add(enemyUnit3);
        }

        public bool HasPlayerWon()
        { return !Units.Any(x => x.FactionType == FactionTypes.Enemy && !x.IsDead()); }

        public bool HasPlayerLost()
        { return !Units.Any(x => x.FactionType == FactionTypes.Player && !x.IsDead()); }

        public void PlayRound()
        {
            Units.Where(x => !x.IsDead())
                .OrderBy(x => x.Stats.Initiative())
                .ToList()
                .ForEach(TakeTurn);
        }

        public void Update(long elapsed)
        {
            if (!HasPlayerWon() && !HasPlayerLost())
            {
                PlayRound();

                if (HasPlayerWon()) { _onGameResolved.OnNext(new GameResolvedEvent(true)); }
                else if (HasPlayerLost()) { _onGameResolved.OnNext(new GameResolvedEvent(false)); }
            }
        }

        public void TakeTurn(Unit unit)
        {
            var target = Units.FirstOrDefault(x => x.FactionType != unit.FactionType && !x.IsDead());
            if (target == null) { return; }

            var damage = RunAttack(unit, target);
            if (target.IsDead()) { ((DefaultClass)unit.Class).Level += 1; }
            _onUnitAttacked?.OnNext(new UnitAttackedEvent(unit, target, damage));
        }

        public byte GenerateAttack(Unit unit)
        { return (byte)(unit.Stats.SlashingDamage() + ((unit.Stats.SlashingDamage() / 5) * unit.Class.Level)); }

        public int RunAttack(Unit attacker, Unit defender)
        {
            var damage = GenerateAttack(attacker);
            if (defender.Stats.Health() >= damage)
            { defender.Stats.Health(defender.Stats.Health() - damage); }
            else
            { defender.Stats.Health(0); }
            return damage;
        }

        public void Dispose()
        {
            _onUnitAttacked?.Dispose();
            _onGameResolved?.Dispose();
        }
    }
}