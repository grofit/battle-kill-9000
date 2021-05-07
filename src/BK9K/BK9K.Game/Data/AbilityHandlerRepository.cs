using System.Collections.Generic;
using BK9K.Game.Handlers.Abilities;
using OpenRpg.Data.Defaults;

namespace BK9K.Game.Data
{
    public class AbilityHandlerRepository : InMemoryDataRepository<IUnitAbilityHandler>, IAbilityHandlerRepository
    {
        public AbilityHandlerRepository(IEnumerable<IUnitAbilityHandler> data) : base(data)
        {
        }
    }
}