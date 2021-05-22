using System.Threading.Tasks;
using BK9K.Mechanics.Units;

namespace BK9K.Mechanics.Handlers
{
    public interface IUnitPhaseHandler
    {
        Task ExecutePhase(Unit unit);
    }
}