using System.Numerics;
using System.Threading.Tasks;
using BK9K.Mechanics.Units;
using OpenRpg.Combat.Attacks;
using OpenRpg.Core.Common;

namespace BK9K.Mechanics.Handlers
{
    public interface IAbilityHandler : IHasDataId
    {
        Task<bool> ExecuteAbility(Unit unit, Unit target = null, Vector2 targetLocation = default);
        Attack CalculateAttack(Unit unit);
    }
}