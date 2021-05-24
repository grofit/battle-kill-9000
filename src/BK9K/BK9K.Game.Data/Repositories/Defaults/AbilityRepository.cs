using System.Collections.Generic;
using BK9K.Mechanics.Types;
using OpenRpg.Combat.Abilities;
using OpenRpg.Data.Defaults;

namespace BK9K.Game.Data.Repositories.Defaults
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