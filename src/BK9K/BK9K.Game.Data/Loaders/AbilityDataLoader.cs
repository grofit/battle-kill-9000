using System.Collections.Generic;
using BK9K.Mechanics.Types.Lookups;
using OpenRpg.Combat.Abilities;

namespace BK9K.Game.Data.Loaders
{
    public class AbilityDataLoader : IDataLoader<Ability>
    {
        public IEnumerable<Ability> LoadData()
        {
            return new List<Ability>
            {
                MakeAttack()
            };
        }

        private Ability MakeAttack()
        {
            return new Ability
            {
                Id = AbilityLookups.Attack,
                NameLocaleId = "Attack",
                DescriptionLocaleId = "Uses the default weapon to attack a nearby unit"
            };
        }
    }
}