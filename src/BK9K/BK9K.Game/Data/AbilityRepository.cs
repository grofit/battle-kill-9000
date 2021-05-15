using System.Collections.Generic;
using BK9K.Framework.Abilities;
using BK9K.Game.Types;
using OpenRpg.Data.Defaults;

namespace BK9K.Game.Data
{
    public class AbilityRepository : InMemoryDataRepository<Ability>, IAbilityRepository
    {
        public AbilityRepository(IEnumerable<Ability> data) : base(data)
        {
            Data = new List<Ability>
            {
                MakeAttack()
            };
        }

        private Ability MakeAttack()
        {
            return new Ability
            {
                Id = AbilityTypes.Attack,
                NameLocaleId = "Attack",
                DescriptionLocaleId = "Uses the default weapon to attack a nearby unit"
            };
        }
    }
}