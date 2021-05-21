using System.Threading.Tasks;
using BK9K.Mechanics.Units;

namespace BK9K.Game.Handlers
{
    public interface IUnitTurnHandler
    {
        Task TakeTurn(Unit unit);
    }
}