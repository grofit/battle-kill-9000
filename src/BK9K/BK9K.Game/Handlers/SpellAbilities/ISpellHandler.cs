using System.Threading.Tasks;
using BK9K.Framework.Spells;
using BK9K.Framework.Transforms;
using OpenRpg.Core.Common;

namespace BK9K.Game.Handlers.SpellAbilities
{
    public interface ISpellHandler : IHasDataId
    {
        Task<bool> ExecuteSpell(Spell spell, Position target);
    }
}