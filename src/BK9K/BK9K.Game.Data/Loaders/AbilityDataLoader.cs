using System.Collections.Generic;
using BK9K.Mechanics.Abilities;
using BK9K.Mechanics.Types.Lookups;
using OpenRpg.Genres.Fantasy.Types;

namespace BK9K.Game.Data.Loaders
{
    public class AbilityDataLoader : IDataLoader<UnitAbility>
    {
        public IEnumerable<UnitAbility> LoadData()
        {
            return new List<UnitAbility>
            {
                MakeAttack(),
                MakeHeal(),
                MakeCleave(),
                MakeIceShard()
            };
        }

        private UnitAbility MakeAttack()
        {
            return new()
            {
                Id = AbilityLookups.Attack,
                IsPassive = false,
                NameLocaleId = "Attack",
                DescriptionLocaleId = "Uses the default weapon to attack a nearby unit",
                DamageType = DamageTypes.UnknownDamage,
                Range = 1,
                Shape = ShapePresets.Empty
            };
        }
        
        private UnitAbility MakeHeal()
        {
            return new()
            {
                Id = AbilityLookups.Heal,
                IsPassive = false,
                NameLocaleId = "Heal",
                DescriptionLocaleId = "Heals a single target based",
                DamageType = DamageTypes.LightDamage,
                Range = 1,
                Shape = ShapePresets.Empty
            };
        }
        
        private UnitAbility MakeCleave()
        {
            return new()
            {
                Id = AbilityLookups.Cleave,
                IsPassive = false,
                NameLocaleId = "Cleave",
                DescriptionLocaleId = "A slightly weaker attack that hurts an enemy and any adjacent enemies",
                DamageType = DamageTypes.UnknownDamage,
                Range = 1,
                Shape = ShapePresets.HorizontalLineShape
            };
        }
        
        private UnitAbility MakeIceShard()
        {
            return new()
            {
                Id = AbilityLookups.IceShard,
                IsPassive = false,
                NameLocaleId = "Ice Shard",
                DescriptionLocaleId = "Fires a shard of ice at the enemy",
                DamageType = DamageTypes.IceDamage,
                Range = 3,
                Shape = ShapePresets.Empty
            };
        }
    }
}