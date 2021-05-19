using System.Collections.Generic;
using BK9K.Framework.Abilities;
using BK9K.Framework.Spells;
using BK9K.Game.Types;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Requirements;
using OpenRpg.Data.Defaults;
using OpenRpg.Genres.Fantasy.Types;

namespace BK9K.Game.Data
{
    public class SpellRepository : InMemoryDataRepository<Spell>, ISpellRepository
    {
        public SpellRepository(IEnumerable<Spell> data) : base(data)
        {
            Data = new List<Spell>
            {
                MakeFirebolt()
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
    }
}