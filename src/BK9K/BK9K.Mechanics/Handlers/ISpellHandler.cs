using System.Numerics;
using System.Threading.Tasks;
using BK9K.Mechanics.Spells;
using OpenRpg.Core.Common;

namespace BK9K.Mechanics.Handlers
{
    public interface ISpellHandler : IHasDataId
    {
        Task<bool> ExecuteSpell(Spell spell, Vector2 target);
    }
}