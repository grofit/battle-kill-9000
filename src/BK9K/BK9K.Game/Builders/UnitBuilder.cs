using System.Linq;
using BK9K.Framework.Equipment;
using BK9K.Framework.Transforms;
using BK9K.Framework.Units;
using BK9K.Game.Data;
using BK9K.Game.Extensions;
using BK9K.Game.Types;
using OpenRpg.Core.Classes;
using OpenRpg.Core.Modifications;
using OpenRpg.Core.Stats;
using OpenRpg.Genres.Fantasy.Extensions;
using OpenRpg.Items;

namespace BK9K.Game.Builders
{
    public class UnitBuilder
    {
        public IRaceTemplateRepository RaceTemplateRepository { get; }
        public IClassTemplateRepository ClassTemplateRepository { get; }
        public IItemTemplateRepository ItemTemplateRepository { get; }
        public IStatsComputer StatsComputer { get; }

        private string _name = "Randy Random";
        private int _initiative = 5;
        private int _level = 1;
        private int _factionType = FactionTypes.Enemy;
        private int _raceType = RaceTypes.Human;
        private int _classType = ClassTypes.Fighter;
        private int _weaponId = ItemTemplateLookups.Unknown;
        private Position _position = Position.Zero;

        public UnitBuilder(IRaceTemplateRepository raceTemplateRepository, IClassTemplateRepository classTemplateRepository, IStatsComputer statsComputer, IItemTemplateRepository itemTemplateRepository)
        {
            RaceTemplateRepository = raceTemplateRepository;
            ClassTemplateRepository = classTemplateRepository;
            StatsComputer = statsComputer;
            ItemTemplateRepository = itemTemplateRepository;
        }

        public UnitBuilder Create()
        { return new(RaceTemplateRepository, ClassTemplateRepository, StatsComputer, ItemTemplateRepository); }

        public UnitBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public UnitBuilder WithInitiative(int initiative)
        {
            _initiative = initiative;
            return this;
        }

        public UnitBuilder WithLevel(int level)
        {
            _level = level;
            return this;
        }
        
        public UnitBuilder WithRace(int raceType)
        {
            _raceType = raceType;
            return this;
        }

        public UnitBuilder WithFaction(int factionType)
        {
            _factionType = factionType;
            return this;
        }

        public UnitBuilder WithClass(int classType, int level = 1)
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

        public UnitBuilder WithWeapon(int itemId)
        {
            _weaponId = itemId;
            return this;
        }

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

            if (_weaponId == ItemTemplateLookups.Unknown)
            {
                if (_classType == ClassTypes.Fighter) { _weaponId = ItemTemplateLookups.Sword; }
                if (_classType == ClassTypes.Rogue) { _weaponId = ItemTemplateLookups.Dagger; }
                if (_classType == ClassTypes.Mage) { _weaponId = ItemTemplateLookups.Staff; }
            }

            if (_weaponId != ItemTemplateLookups.Unknown)
            {
                var weaponTemplate = ItemTemplateRepository.Retrieve(_weaponId);
                var weapon = new DefaultItem
                {
                    ItemTemplate = weaponTemplate,
                    Modifications = new IModification[0],
                    Variables = new DefaultItemVariables()
                };
                var didEquip = unit.Equipment.MainHandSlot.EquipItemToSlot(weapon);
                if(didEquip){}
            }

            var unitEffects = unit.GetActiveEffects().ToList();
            unit.Stats = StatsComputer.ComputeStats(unitEffects);
            unit.Stats.Initiative(_initiative);
            return unit;
        }
    }
}