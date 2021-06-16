using System.Linq;
using System.Numerics;
using BK9K.Game.Data.Repositories;
using BK9K.Mechanics.Extensions;
using BK9K.Mechanics.Types;
using BK9K.Mechanics.Types.Lookups;
using BK9K.Mechanics.Units;
using OpenRpg.Core.Classes;
using OpenRpg.Core.Modifications;
using OpenRpg.Core.Stats;
using OpenRpg.Genres.Fantasy.Defaults;
using OpenRpg.Genres.Fantasy.Extensions;
using OpenRpg.Items;

namespace BK9K.Game.Data.Builders
{
    public class UnitBuilder
    {
        public IRaceTemplateRepository RaceTemplateRepository { get; }
        public IClassTemplateRepository ClassTemplateRepository { get; }
        public IItemTemplateRepository ItemTemplateRepository { get; }
        public IAbilityRepository AbilityRepository { get; }
        public IStatsComputer StatsComputer { get; }

        private string _name = "Randy Random";
        private int _id = 0;
        private int _initiative = 5;
        private int _level = 1;
        private int _factionType = FactionTypes.Enemy;
        private int _raceType = RaceLookups.Human;
        private int _classType = ClassLookups.Fighter;
        private int _weaponId = ItemTemplateLookups.Unknown;
        private int _armourId = ItemTemplateLookups.Unknown;
        private int _abilityId = AbilityLookups.Attack;

        private Vector2 _position = Vector2.Zero;

        public UnitBuilder(IRaceTemplateRepository raceTemplateRepository, IClassTemplateRepository classTemplateRepository, IStatsComputer statsComputer, IItemTemplateRepository itemTemplateRepository, IAbilityRepository abilityRepository)
        {
            RaceTemplateRepository = raceTemplateRepository;
            ClassTemplateRepository = classTemplateRepository;
            StatsComputer = statsComputer;
            ItemTemplateRepository = itemTemplateRepository;
            AbilityRepository = abilityRepository;
        }

        public UnitBuilder Create()
        { return new(RaceTemplateRepository, ClassTemplateRepository, StatsComputer, ItemTemplateRepository, AbilityRepository); }

        public UnitBuilder WithId(int id)
        {
            _id = id;
            return this;
        }
        
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

        public UnitBuilder WithPosition(Vector2 position)
        {
            _position = position;
            return this;
        }

        public UnitBuilder WithPosition(int x, int y)
        { return WithPosition(new Vector2(x, y)); }

        public UnitBuilder WithWeapon(int itemId)
        {
            _weaponId = itemId;
            return this;
        }

        public UnitBuilder WithArmour(int itemId)
        {
            _armourId = itemId;
            return this;
        }

        public UnitBuilder WithAbility(int abilityId)
        {
            _abilityId = abilityId;
            return this;
        }

        public Unit Build()
        {
            var classTemplate = ClassTemplateRepository.Retrieve(_classType);
            var raceTemplate = RaceTemplateRepository.Retrieve(_raceType);
            var ability = AbilityRepository.Retrieve(_abilityId);
            
            var unit = _factionType == FactionTypes.Enemy ? new EnemyUnit() : new Unit();

            unit.Id = _id;
            unit.NameLocaleId = _name;
            unit.FactionType = _factionType;
            unit.Position = _position;
            unit.Race = raceTemplate;
            unit.ActiveAbilities.Add(ability);
            unit.Class = new DefaultClass(_level, classTemplate);
            unit.Equipment = new DefaultEquipment();

            var weapon = GetWeapon();
            if(weapon != null)
            { unit.Equipment.MainHandSlot.EquipItemToSlot(weapon); }

            var armour = GetArmour();
            if(armour != null)
            { unit.Equipment.UpperBodySlot.EquipItemToSlot(armour); }
            
            var unitEffects = unit.GetActiveEffects().ToList();
            unit.Stats = StatsComputer.ComputeStats(unitEffects);
            unit.Stats.Initiative(_initiative);
            return unit;
        }

        private IItem GetWeapon()
        {
            if (_weaponId == ItemTemplateLookups.Unknown)
            {
                if (_classType == ClassLookups.Fighter)
                { _weaponId = ItemTemplateLookups.Sword; }

                if (_classType == ClassLookups.Rogue)
                { _weaponId = ItemTemplateLookups.Dagger; }

                if (_classType == ClassLookups.Mage)
                { _weaponId = ItemTemplateLookups.Staff; }
            }

            if (_weaponId == ItemTemplateLookups.Unknown)
            { return null; }

            var weaponTemplate = ItemTemplateRepository.Retrieve(_weaponId);

            return new DefaultItem
            {
                ItemTemplate = weaponTemplate,
                Modifications = new IModification[0],
                Variables = new DefaultItemVariables()
            };
        }

        private IItem GetArmour()
        {
            if (_armourId == ItemTemplateLookups.Unknown)
            {
                if (_classType == ClassLookups.Fighter)
                { _armourId = ItemTemplateLookups.PlateArmour; }

                if (_classType == ClassLookups.Rogue)
                { _armourId = ItemTemplateLookups.Tunic; }

                if (_classType == ClassLookups.Mage)
                { _armourId = ItemTemplateLookups.Robe; }
            }

            if (_armourId == ItemTemplateLookups.Unknown)
            { return null; }

            var armourTemplate = ItemTemplateRepository.Retrieve(_armourId);
            return new DefaultItem
            {
                ItemTemplate = armourTemplate,
                Modifications = new IModification[0],
                Variables = new DefaultItemVariables()
            };

        }
    }
}