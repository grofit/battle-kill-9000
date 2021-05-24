using BK9K.Game.Data.Datasets;
using OpenRpg.Combat.Abilities;
using OpenRpg.Data.Defaults;

namespace BK9K.Game.Data.Repositories.Defaults
{
    public class AbilityRepository : InMemoryDataRepository<Ability>, IAbilityRepository
    {
        public AbilityRepository()
        { Data = new AbilityDataset().GetDataset(); }
    }
}