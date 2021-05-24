using System.Collections.Generic;
using BK9K.Mechanics.Types;
using OpenRpg.Cards.Effects;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Requirements;
using OpenRpg.Data.Defaults;
using OpenRpg.Genres.Fantasy.Types;

namespace BK9K.Game.Data.Repositories.Defaults
{
    public class CardEffectsRepository : InMemoryDataRepository<CardEffects>, ICardEffectsRepository
    {
        public CardEffectsRepository()
        {
            Data = new List<CardEffects>
            {
                MakeMinorStrengthEffects(),
                MakeMinorIntelligenceEffects()
            };
        }

        private CardEffects MakeMinorStrengthEffects()
        {
            var effects = new List<Effect>
            {
                new() {EffectType = EffectTypes.StrengthBonusAmount, Potency = 1, Requirements = new Requirement[0]}
            };
            return new CardEffects
            {
                Id = NamedEffectsTypes.MinorStrength,
                NameLocaleId = "Minor Strength",
                DescriptionLocaleId = "The card imbues the unit with a minor increase in strength",
                Effects = effects
            };
        }

        private CardEffects MakeMinorIntelligenceEffects()
        {
            var effects = new List<Effect>
            {
                new() {EffectType = EffectTypes.IntelligenceBonusAmount, Potency = 1, Requirements = new Requirement[0]}
            };
            return new CardEffects
            {
                Id = NamedEffectsTypes.MinorIntelligence,
                NameLocaleId = "Minor Intelligence",
                DescriptionLocaleId = "The card imbues the unit with a minor increase in intelligence",
                Effects = effects
            };
        }
    }
}