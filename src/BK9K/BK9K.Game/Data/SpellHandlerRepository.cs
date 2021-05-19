using System.Collections.Generic;
using BK9K.Game.Handlers.SpellAbilities;
using OpenRpg.Data.Defaults;

namespace BK9K.Game.Data
{
    public class SpellHandlerRepository : InMemoryDataRepository<ISpellHandler>, ISpellHandlerRepository
    {
        public SpellHandlerRepository(IEnumerable<ISpellHandler> data) : base(data)
        {
        }
    }
}