using System.Threading.Tasks;
using BK9K.Framework.Transforms;
using OpenRpg.Core.Common;

namespace BK9K.Game.Handlers.SpellAbilities
{
    public interface ISpellAbilityHandler : IHasDataId
    {
        Task ExecuteAbility(Position target);
    }
}