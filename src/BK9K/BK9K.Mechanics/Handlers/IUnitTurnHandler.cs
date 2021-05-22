using System.Threading.Tasks;
using BK9K.Mechanics.Units;

namespace BK9K.Mechanics.Handlers
{
    public interface IUnitTurnHandler
    {
        Task TakeTurn(Unit unit);
    }
}