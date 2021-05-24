using System.Collections.Generic;
using BK9K.Mechanics.Extensions;
using BK9K.Mechanics.Types;
using BK9K.Mechanics.Types.Lookups;
using OpenRpg.Cards.Effects;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Races;
using OpenRpg.Genres.Fantasy.Types;

namespace BK9K.Game.Data.Datasets
{
    public class RaceTemplateDataset : IDataset<IRaceTemplate>
    {
        public List<IRaceTemplate> GetDataset()
        {
            return new List<IRaceTemplate>
            {
                GenerateHumanTemplate(),
                GenerateElfTemplate(),
                GenerateDwarfTemplate(),
                GenerateEnemyTemplate()
            };
        }

        private IRaceTemplate GenerateHumanTemplate()
        {
            var effects = new[]
            {
                new Effect {Potency = 10, EffectType = EffectTypes.AllAttributeBonusAmount},
                new Effect {Potency = 15, EffectType = EffectTypes.HealthBonusAmount}
            };

            var template = new DefaultRaceTemplate
            {
                Id = RaceLookups.Human,
                NameLocaleId = "Human",
                DescriptionLocaleId = "Humans are the most common of all races",
                Effects = effects
            };

            template.Variables.AssetCode("race-human");
            return template;
        }

        private IRaceTemplate GenerateElfTemplate()
        {
            var effects = new[]
            {
                new Effect {Potency = 8, EffectType = EffectTypes.StrengthBonusAmount},
                new Effect {Potency = 12, EffectType = EffectTypes.DexterityBonusAmount},
                new Effect {Potency = 8, EffectType = EffectTypes.ConstitutionBonusAmount},
                new Effect {Potency = 12, EffectType = EffectTypes.IntelligenceBonusAmount},
                new Effect {Potency = 10, EffectType = EffectTypes.WisdomBonusAmount},
                new Effect {Potency = 10, EffectType = EffectTypes.HealthBonusAmount}
            };

            var template = new DefaultRaceTemplate
            {
                Id = RaceLookups.Elf,
                NameLocaleId = "Elf",
                DescriptionLocaleId = "Elves are pretty common, have pointy ears too",
                Effects = effects
            };

            template.Variables.AssetCode("race-elf");
            return template;
        }

        private IRaceTemplate GenerateDwarfTemplate()
        {
            var effects = new[]
            {
                new Effect {Potency = 12, EffectType = EffectTypes.StrengthBonusAmount},
                new Effect {Potency = 8, EffectType = EffectTypes.DexterityBonusAmount},
                new Effect {Potency = 12, EffectType = EffectTypes.ConstitutionBonusAmount},
                new Effect {Potency = 10, EffectType = EffectTypes.IntelligenceBonusAmount},
                new Effect {Potency = 10, EffectType = EffectTypes.WisdomBonusAmount},
                new Effect {Potency = 20, EffectType = EffectTypes.HealthBonusAmount},
            };

            var template = new DefaultRaceTemplate
            {
                Id = RaceLookups.Dwarf,
                NameLocaleId = "Dwarf",
                DescriptionLocaleId = "Dwarves are strong and hardy",
                Effects = effects
            };

            template.Variables.AssetCode("race-dwarf");
            return template;
        }

        private IRaceTemplate GenerateEnemyTemplate()
        {
            var effects = new Effect[0];

            var template = new DefaultRaceTemplate
            {
                Id = RaceLookups.Enemy,
                NameLocaleId = "Enemy",
                DescriptionLocaleId = "Enemies are always hostile",
                Effects = effects
            };

            template.Variables.AssetCode("race-enemy");
            return template;
        }
    }
}