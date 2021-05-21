using BK9K.Mechanics.Spells;
using OpenRpg.Data.Repositories;

namespace BK9K.Game.Data
{
    public interface ISpellRepository : IReadRepository<Spell, int>
    {
    }
}