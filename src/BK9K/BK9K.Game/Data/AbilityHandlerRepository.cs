using System.Collections.Generic;
using BK9K.Game.Handlers.UnitAbilities;
using OpenRpg.Data.Defaults;

namespace BK9K.Game.Data
{
    public class AbilityHandlerRepository : InMemoryDataRepository<IAbilityHandler>, IAbilityHandlerRepository
    {
        public AbilityHandlerRepository(IEnumerable<IAbilityHandler> data) : base(data)
        {
        }
    }
}