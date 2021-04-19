namespace BK9K.Framework.Units
{
    public class UnitBuilder
    {
        private string _name = "Randy Random";
        private byte _health { get; set; } = 100;
        private byte _attack { get; set; } = 10;

        public static UnitBuilder Create()
        { return new UnitBuilder(); }

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

        public Unit Build()
        {
            return new Unit
            {
                Name = _name,
                Attack = _attack,
                Health = _health
            };
        }
    }
}