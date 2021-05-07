using System.Threading.Tasks;
using BK9K.Framework.Units;
using OpenRpg.Core.Common;

namespace BK9K.Game.Handlers.Abilities
{
    public interface IUnitAbilityHandler : IHasDataId
    {
        Task ExecuteAbility(Unit unit);
    }
}