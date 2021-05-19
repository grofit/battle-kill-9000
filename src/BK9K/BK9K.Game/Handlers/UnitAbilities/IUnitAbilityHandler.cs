using System.Threading.Tasks;
using BK9K.Framework.Units;
using OpenRpg.Core.Common;

namespace BK9K.Game.Handlers.UnitAbilities
{
    public interface IUnitAbilityHandler : IHasDataId
    {
        Task ExecuteAbility(Unit unit);
    }
}