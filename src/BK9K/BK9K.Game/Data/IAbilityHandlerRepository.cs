using BK9K.Game.Handlers.UnitAbilities;
using OpenRpg.Data.Repositories;

namespace BK9K.Game.Data
{
    public interface IAbilityHandlerRepository : IReadRepository<IUnitAbilityHandler, int>
    {

    }
}