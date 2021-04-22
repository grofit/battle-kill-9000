using BK9K.Framework.Transforms;
using BK9K.Framework.Types;

namespace BK9K.Framework.Units
{
    public class UnitBuilder
    {
        private string _name = "Randy Random";
        private byte _initiative = 5;
        private byte _level = 1;
        private byte _factionType = FactionTypes.Enemy;
        private byte _classType = ClassTypes.Fighter;
        private byte _health = 100;
        private byte _attack = 10;
        private Position _position = Position.Zero;

        public static UnitBuilder Create()
        { return new(); }

        public UnitBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public UnitBuilder WithHealth(byte health)
        {
            _health = health;
            return this;
        }

        public UnitBuilder WithAttack(byte attack)
        {
            _attack = attack;
            return this;
        }

        public UnitBuilder WithInitiative(byte initiative)
        {
            _initiative = initiative;
            return this;
        }

        public UnitBuilder WithLevel(byte level)
        {
            _level = level;
            return this;
        }

        public UnitBuilder WithFaction(byte factionType)
        {
            _factionType = factionType;
            return this;
        }

        public UnitBuilder WithClass(byte classType)
        {
            _classType = classType;
            return this;
        }

        public UnitBuilder WithPosition(Position position)
        {
            _position = position;
            return this;
        }

        public UnitBuilder WithPosition(int x, int y)
        { return WithPosition(new Position(x, y)); }

        public Unit Build()
        {
            return new Unit
            {
                Name = _name,
                Attack = _attack,
                Health = _health,
                FactionType = _factionType,
                ClassType = _classType,
                Initiative = _initiative,
                Level = _level,
                Position = _position
            };
        }
    }
}