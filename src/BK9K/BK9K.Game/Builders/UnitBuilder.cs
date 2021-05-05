using System.Linq;
using BK9K.Framework.Equipment;
using BK9K.Framework.Extensions;
using BK9K.Framework.Transforms;
using BK9K.Framework.Types;
using BK9K.Framework.Units;
using BK9K.Game.Data;
using OpenRpg.Core.Classes;
using OpenRpg.Core.Races;
using OpenRpg.Core.Stats;
using OpenRpg.Genres.Fantasy.Extensions;

namespace BK9K.Game.Builders
{
    public class UnitBuilder
    {
        public IRaceTemplateRepository RaceTemplateRepository { get; }
        public IClassTemplateRepository ClassTemplateRepository { get; }
        public IStatsComputer StatsComputer { get; }

        private string _name = "Randy Random";
        private byte _initiative = 5;
        private byte _level = 1;
        private byte _factionType = FactionTypes.Enemy;
        private byte _raceType = RaceTypes.Human;
        private byte _classType = ClassTypes.Fighter;
        private Position _position = Position.Zero;

        public UnitBuilder(IRaceTemplateRepository raceTemplateRepository, IClassTemplateRepository classTemplateRepository, IStatsComputer statsComputer)
        {
            RaceTemplateRepository = raceTemplateRepository;
            ClassTemplateRepository = classTemplateRepository;
            StatsComputer = statsComputer;
        }

        public UnitBuilder Create()
        { return new(RaceTemplateRepository, ClassTemplateRepository, StatsComputer); }

        public UnitBuilder WithName(string name)
        {
            _name = name;
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
        
        public UnitBuilder WithRace(byte raceType)
        {
            _raceType = raceType;
            return this;
        }

        public UnitBuilder WithFaction(byte factionType)
        {
            _factionType = factionType;
            return this;
        }

        public UnitBuilder WithClass(byte classType, byte level = 1)
        {
            _classType = classType;
            _level = level;
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
            var classTemplate = ClassTemplateRepository.Retrieve(_classType);
            var raceTemplate = RaceTemplateRepository.Retrieve(_raceType);
            var unit = new Unit
            {
                NameLocaleId = _name,
                FactionType = _factionType,
                Position = _position,
                Race = raceTemplate,
                Class = new DefaultClass(_level, classTemplate),
                Equipment = new DefaultEquipment()
            };
            unit.Stats = StatsComputer.ComputeStats(unit.GetActiveEffects().ToList());
            unit.Stats.Initiative(_initiative);
            return unit;
        }
    }
}