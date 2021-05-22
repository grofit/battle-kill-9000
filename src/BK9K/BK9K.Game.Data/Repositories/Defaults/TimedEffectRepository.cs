using System.Collections.Generic;
using BK9K.Mechanics.Types;
using OpenRpg.Combat.Effects;
using OpenRpg.Core.Requirements;
using OpenRpg.Data.Defaults;
using OpenRpg.Genres.Fantasy.Types;

namespace BK9K.Game.Data.Repositories.Defaults
{
    public class TimedEffectRepository : InMemoryDataRepository<TimedEffect>, ITimedEffectRepository
    {
        public TimedEffectRepository(IEnumerable<TimedEffect> data) : base(data)
        {
            Data = new List<TimedEffect>
            {
                MakeMinorRegen(),
                MakePoison()
            };
        }
        
        private TimedEffect MakeMinorRegen()
        {
            return new TimedEffect()
            {
                Id = TimedEffectTypes.MinorRegen,
                NameLocaleId = "Minor Regen",
                DescriptionLocaleId = "Regenerate a small amount of HP over time",
                EffectType = EffectTypes.HealthRestoreAmount,
                Potency = 5,
                MaxStack = 2,
                Duration = 10,
                Frequency = 1,
                Requirements = new Requirement[0]
            };
        }

        private TimedEffect MakePoison()
        {
            return new TimedEffect()
            {
                Id = TimedEffectTypes.MinorPoison,
                NameLocaleId = "Minor Poison",
                DescriptionLocaleId = "Reduce HP slowly over time",
                EffectType = EffectTypes.DarkDamageAmount,
                Potency = 3,
                MaxStack = 3,
                Duration = 7,
                Frequency = 0.5f,
                Requirements = new Requirement[0]
            };
        }
    }
}