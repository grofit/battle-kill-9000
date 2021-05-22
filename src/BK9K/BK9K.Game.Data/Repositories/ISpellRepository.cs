using BK9K.Mechanics.Spells;
using OpenRpg.Data.Repositories;

namespace BK9K.Game.Data.Repositories
{
    public interface ISpellRepository : IReadRepository<Spell, int>
    {
    }
}