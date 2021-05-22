using System.Threading.Tasks;
using BK9K.Mechanics.Units;
using OpenRpg.Core.Common;

namespace BK9K.Mechanics.Handlers
{
    public interface IAbilityHandler : IHasDataId
    {
        Task<bool> ExecuteAbility(Unit unit);
    }
}