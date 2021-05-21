using BK9K.Mechanics.Abilities;
using OpenRpg.Data.Repositories;

namespace BK9K.Game.Data
{
    public interface IAbilityRepository : IReadRepository<Ability, int>
    {
    }
}