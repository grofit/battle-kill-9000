using System.Threading.Tasks;
using BK9K.Mechanics.Units;
using OpenRpg.Core.Common;

namespace BK9K.Game.Handlers.UnitAbilities
{
    public interface IAbilityHandler : IHasDataId
    {
        Task<bool> ExecuteAbility(Unit unit);
    }
}