using System.Collections.Generic;
using BK9K.Mechanics.Handlers;
using OpenRpg.Data.Defaults;

namespace BK9K.Game.Data.Repositories.Defaults
{
    public class SpellHandlerRepository : InMemoryDataRepository<ISpellHandler>, ISpellHandlerRepository
    {
        public SpellHandlerRepository(IEnumerable<ISpellHandler> data) : base(data)
        {
        }
    }
}