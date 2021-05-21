using System.Threading.Tasks;
using BK9K.Mechanics.Units;

namespace BK9K.Game.Handlers.Phases
{
    public interface IUnitPhaseHandler
    {
        Task ExecutePhase(Unit unit);
    }
}