using System.Linq;
using BK9K.Game.Data.Datasets;
using OpenRpg.Combat.Effects;
using OpenRpg.Data.Defaults;

namespace BK9K.Game.Data.Repositories.Defaults
{
    public class TimedEffectRepository : InMemoryDataRepository<TimedEffect>, ITimedEffectRepository
    {
        public TimedEffectRepository()
        { Data = new TimedEffectDataset().GetDataset(); }
    }
}