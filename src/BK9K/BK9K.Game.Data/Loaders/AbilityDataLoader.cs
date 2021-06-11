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
                MakeAttack()
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
                Size = 1
            };
        }
    }
}