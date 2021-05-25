using System.Collections.Generic;
using OpenRpg.Combat.Abilities;
using OpenRpg.Data.Defaults;

namespace BK9K.Game.Data.Repositories.Defaults
{
    public class AbilityRepository : InMemoryDataRepository<Ability>, IAbilityRepository
    {
        public AbilityRepository(IEnumerable<Ability> data) : base(data)
        {}
    }
}