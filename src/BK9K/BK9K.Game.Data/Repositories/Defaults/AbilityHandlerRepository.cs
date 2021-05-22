using System.Collections.Generic;
using BK9K.Mechanics.Handlers;
using OpenRpg.Data.Defaults;

namespace BK9K.Game.Data.Repositories.Defaults
{
    public class AbilityHandlerRepository : InMemoryDataRepository<IAbilityHandler>, IAbilityHandlerRepository
    {
        public AbilityHandlerRepository(IEnumerable<IAbilityHandler> data) : base(data)
        {
        }
    }
}