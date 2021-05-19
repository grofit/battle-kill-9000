using BK9K.Game.Handlers.SpellAbilities;
using OpenRpg.Data.Repositories;

namespace BK9K.Game.Data
{
    public interface ISpellHandlerRepository : IReadRepository<ISpellHandler, int>
    {

    }
}