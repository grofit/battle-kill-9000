using System.Collections.Generic;
using BK9K.Mechanics.Spells;
using BK9K.Mechanics.Types.Lookups;
using OpenRpg.Core.Effects;
using OpenRpg.Data.Defaults;
using OpenRpg.Genres.Fantasy.Types;

namespace BK9K.Game.Data.Repositories.Defaults
{
    public class SpellRepository : InMemoryDataRepository<Spell>, ISpellRepository
    {
        public ITimedEffectRepository TimedEffectRepository { get; }

        public SpellRepository(ITimedEffectRepository timedEffectRepository)
        {
            TimedEffectRepository = timedEffectRepository;
            Data = new List<Spell>
            {
                MakeFirebolt(),
                MakeMinorRegen()
            };
        }

        private Spell MakeFirebolt()
        {
            var fireEffect = new Effect()
            {
                EffectType = EffectTypes.FireDamageAmount,
                Potency = 30
            };

            return new Spell
            {
                Id = SpellLookups.Firebolt,
                NameLocaleId = "Firebolt",
                DescriptionLocaleId = "Incinerates a foe with a bolt of fire",
                Effects = new []{ fireEffect }
            };
        }

        private Spell MakeMinorRegen()
        {
            var timedEffect = TimedEffectRepository.Retrieve(TimedEffectLookups.MinorRegen);
            return new Spell
            {
                Id = SpellLookups.MinorRegen,
                NameLocaleId = timedEffect.NameLocaleId,
                DescriptionLocaleId = timedEffect.DescriptionLocaleId,
                Effects = new[] { timedEffect }
            };
        }

        private Spell MakeMinorPosion()
        {
            var timedEffect = TimedEffectRepository.Retrieve(TimedEffectLookups.MinorPoison);
            return new Spell
            {
                Id = SpellLookups.MinorPoison,
                NameLocaleId = timedEffect.NameLocaleId,
                DescriptionLocaleId = timedEffect.DescriptionLocaleId,
                Effects = new[] { timedEffect }
            };
        }
    }
}