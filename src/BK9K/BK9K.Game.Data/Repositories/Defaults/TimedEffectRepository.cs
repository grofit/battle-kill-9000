using System.Collections.Generic;
using OpenRpg.Combat.Effects;
using OpenRpg.Data.Defaults;

namespace BK9K.Game.Data.Repositories.Defaults
{
    public class TimedEffectRepository : InMemoryDataRepository<TimedEffect>, ITimedEffectRepository
    {
        public TimedEffectRepository(IEnumerable<TimedEffect> data) : base(data)
        {}
    }
}