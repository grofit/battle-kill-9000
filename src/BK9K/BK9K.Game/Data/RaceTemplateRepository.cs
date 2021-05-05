using System.Collections.Generic;
using BK9K.Framework.Types;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Races;
using OpenRpg.Core.Requirements;
using OpenRpg.Data.Defaults;
using OpenRpg.Genres.Fantasy.Types;

namespace BK9K.Game.Data
{
    public class RaceTemplateRepository : InMemoryDataRepository<IRaceTemplate>, IRaceTemplateRepository
    {
        public RaceTemplateRepository()
        {
            Data = new List<IRaceTemplate>
            {
                GenerateHumanTemplate(),
                GenerateElfTemplate(),
                GenerateDwarfTemplate(),
            };
        }

        private IRaceTemplate GenerateHumanTemplate()
        {
            var effects = new[]
            {
                new Effect {Potency = 10, EffectType = EffectTypes.AllAttributeBonusAmount},
                new Effect {Potency = 80, EffectType = EffectTypes.HealthBonusAmount}
            };

            return new DefaultRaceTemplate
            {
                Id = RaceTypes.Human,
                AssetCode = "race-human",
                NameLocaleId = "Human",
                DescriptionLocaleId = "Humans are the most common of all races",
                Effects = effects,
                Requirements = new List<Requirement>()
            };
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
                new Effect {Potency = 70, EffectType = EffectTypes.HealthBonusAmount}
            };

            return new DefaultRaceTemplate
            {
                Id = RaceTypes.Elf,
                AssetCode = "race-elf",
                NameLocaleId = "Elf",
                DescriptionLocaleId = "Elves are pretty common, have pointy ears too",
                Effects = effects,
                Requirements = new List<Requirement>()
            };
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
                new Effect {Potency = 100, EffectType = EffectTypes.HealthBonusAmount},
            };

            return new DefaultRaceTemplate
            {
                Id = RaceTypes.Dwarf,
                AssetCode = "race-dwarf",
                NameLocaleId = "Dwarf",
                DescriptionLocaleId = "Dwarves are strong and hardy",
                Effects = effects,
                Requirements = new List<Requirement>()
            };
        }
    }
}