using System.Collections.Generic;
using BK9K.Mechanics.Abilities;
using OpenRpg.Combat.Abilities;
using OpenRpg.Data.Defaults;

namespace BK9K.Game.Data.Repositories.Defaults
{
    public class AbilityRepository : InMemoryDataRepository<UnitAbility>, IAbilityRepository
    {
        public AbilityRepository(IEnumerable<UnitAbility> data) : base(data)
        {}
    }
}