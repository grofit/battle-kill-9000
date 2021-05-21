using System.Collections.Generic;
using BK9K.Mechanics.Spells;
using BK9K.Mechanics.Types;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Requirements;
using OpenRpg.Data.Defaults;
using OpenRpg.Genres.Fantasy.Types;

namespace BK9K.Game.Data
{
    public class SpellRepository : InMemoryDataRepository<Spell>, ISpellRepository
    {
        public ITimedEffectRepository TimedEffectRepository { get; }

        public SpellRepository(IEnumerable<Spell> data, ITimedEffectRepository timedEffectRepository) : base(data)
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
                Potency = 30,
                Requirements = new Requirement[0]
            };

            return new Spell
            {
                Id = SpellTypes.Firebolt,
                NameLocaleId = "Firebolt",
                DescriptionLocaleId = "Incinerates a foe with a bolt of fire",
                Effects = new []{ fireEffect }
            };
        }

        private Spell MakeMinorRegen()
        {
            var timedEffect = TimedEffectRepository.Retrieve(TimedEffectTypes.MinorRegen);
            return new Spell
            {
                Id = SpellTypes.MinorRegen,
                NameLocaleId = timedEffect.NameLocaleId,
                DescriptionLocaleId = timedEffect.DescriptionLocaleId,
                Effects = new[] { timedEffect }
            };
        }

        private Spell MakeMinorPosion()
        {
            var timedEffect = TimedEffectRepository.Retrieve(TimedEffectTypes.MinorPoison);
            return new Spell
            {
                Id = SpellTypes.MinorPoison,
                NameLocaleId = timedEffect.NameLocaleId,
                DescriptionLocaleId = timedEffect.DescriptionLocaleId,
                Effects = new[] { timedEffect }
            };
        }
    }
}