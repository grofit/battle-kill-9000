using System.Collections.Generic;
using BK9K.Mechanics.Types;
using OpenRpg.Cards.Effects;
using OpenRpg.Core.Effects;
using OpenRpg.Genres.Fantasy.Types;

namespace BK9K.Game.Data.Loaders
{
    public class CardEffectsDataLoader : IDataLoader<CardEffects>
    {
        public IEnumerable<CardEffects> LoadData()
        {
            return new List<CardEffects>
            {
                MakeMinor(CardEffectLookups.MinorStrength, EffectTypes.StrengthBonusAmount, "Strength"),
                MakeMinor(CardEffectLookups.MinorConstitution, EffectTypes.ConstitutionBonusAmount, "Constitution"),
                MakeMinor(CardEffectLookups.MinorDexterity, EffectTypes.DexterityBonusAmount, "Dexterity"),
                MakeMinor(CardEffectLookups.MinorIntelligence, EffectTypes.IntelligenceBonusAmount, "Intelligence"),
                MakeMinor(CardEffectLookups.MinorWisdom, EffectTypes.WisdomBonusAmount, "Wisdom"),

                MakeMajor(CardEffectLookups.MajorStrength, EffectTypes.StrengthBonusAmount, "Strength"),
                MakeMajor(CardEffectLookups.MajorConstitution, EffectTypes.ConstitutionBonusAmount, "Constitution"),
                MakeMajor(CardEffectLookups.MajorDexterity, EffectTypes.DexterityBonusAmount, "Dexterity"),
                MakeMajor(CardEffectLookups.MajorIntelligence, EffectTypes.IntelligenceBonusAmount, "Intelligence"),
                MakeMajor(CardEffectLookups.MajorWisdom, EffectTypes.WisdomBonusAmount, "Wisdom"),
            };
        }

        private CardEffects MakeMinor(int cardEffectType, int effectType, string effectName)
        { return MakeStatEffectCard(cardEffectType, effectType, 1, effectName, "Minor"); }

        private CardEffects MakeMajor(int cardEffectType, int effectType, string effectName)
        { return MakeStatEffectCard(cardEffectType, effectType, 2, effectName, "Major"); }

        private CardEffects MakeStatEffectCard(int cardEffectType, int effectType, int potency, string effectName, string prefix)
        {
            var effects = new List<Effect>
            {
                new() { EffectType = effectType, Potency = potency }
            };
            return new CardEffects
            {
                Id = cardEffectType,
                NameLocaleId = $"{prefix} {effectName}",
                DescriptionLocaleId = $"The card imbues the unit with a {prefix} increase in {effectName}",
                Effects = effects
            };
        }
    }
}