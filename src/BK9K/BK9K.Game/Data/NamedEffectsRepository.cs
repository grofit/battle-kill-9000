using System.Collections.Generic;
using BK9K.Framework.Effects;
using BK9K.Game.Types;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Requirements;
using OpenRpg.Data.Defaults;
using OpenRpg.Genres.Fantasy.Types;

namespace BK9K.Game.Data
{
    public class NamedEffectsRepository : InMemoryDataRepository<NamedEffects>, INamedEffectsRepository
    {
        public NamedEffectsRepository()
        {
            Data = new List<NamedEffects>
            {
                MakeMinorStrengthEffects(),
                MakeMinorIntelligenceEffects()
            };
        }

        private NamedEffects MakeMinorStrengthEffects()
        {
            var effects = new List<Effect>
            {
                new() {EffectType = EffectTypes.StrengthBonusAmount, Potency = 1, Requirements = new Requirement[0]}
            };
            return new NamedEffects
            {
                Id = NamedEffectsTypes.MinorStrength,
                NameLocaleId = "Minor Strength",
                DescriptionLocaleId = "The card imbues the unit with a minor increase in strength",
                Effects = effects
            };
        }

        private NamedEffects MakeMinorIntelligenceEffects()
        {
            var effects = new List<Effect>
            {
                new() {EffectType = EffectTypes.IntelligenceBonusAmount, Potency = 1, Requirements = new Requirement[0]}
            };
            return new NamedEffects
            {
                Id = NamedEffectsTypes.MinorIntelligence,
                NameLocaleId = "Minor Intelligence",
                DescriptionLocaleId = "The card imbues the unit with a minor increase in intelligence",
                Effects = effects
            };
        }
    }
}