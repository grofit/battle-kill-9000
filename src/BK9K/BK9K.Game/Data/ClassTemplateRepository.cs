using System.Collections.Generic;
using BK9K.Game.Types;
using OpenRpg.Core.Classes;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Requirements;
using OpenRpg.Data.Defaults;
using OpenRpg.Genres.Fantasy.Types;

namespace BK9K.Game.Data
{
    public class ClassTemplateRepository : InMemoryDataRepository<IClassTemplate>, IClassTemplateRepository
    {
        public ClassTemplateRepository()
        {
            Data = new List<IClassTemplate>
            {
                GenerateFighterClass(),
                GenerateMageClass(),
                GenerateRogueClass(),
                GenerateMonsterClass()
            };
        }

        private IClassTemplate GenerateFighterClass()
        {
            var effects = new[]
            {
                new Effect {Potency = 2, EffectType = EffectTypes.StrengthBonusAmount},
                new Effect {Potency = 2, EffectType = EffectTypes.ConstitutionBonusAmount},
                new Effect {Potency = 20, EffectType = EffectTypes.AllMeleeAttackBonusPercentage},
                new Effect {Potency = 30, EffectType = EffectTypes.AllMeleeDefenseBonusPercentage},
                new Effect {Potency = 10, EffectType = EffectTypes.HealthBonusAmount}
            };

            return new DefaultClassTemplate
            {
                Id = ClassTypes.Fighter,
                AssetCode = "class-fighter",
                NameLocaleId = "Fighter",
                DescriptionLocaleId = "Super tough, hits things",
                Effects = effects,
                Requirements = new Requirement[0]
            };
        }

        private IClassTemplate GenerateMageClass()
        {
            var effects = new[]
            {
                new Effect {Potency = 4, EffectType = EffectTypes.IntelligenceBonusAmount},
                new Effect {Potency = 4, EffectType = EffectTypes.AllMeleeAttackBonusPercentage},
                new Effect {Potency = 30, EffectType = EffectTypes.AllElementDefenseBonusPercentage},
                new Effect {Potency = 30, EffectType = EffectTypes.MagicBonusPercentage}
            };

            return new DefaultClassTemplate
            {
                Id = ClassTypes.Mage,
                AssetCode = "class-mage",
                NameLocaleId = "Mage",
                DescriptionLocaleId = "Powerful magic users",
                Effects = effects,
                Requirements = new Requirement[0]
            };
        }
        
        private IClassTemplate GenerateRogueClass()
        {
            var effects = new[]
            {
                new Effect {Potency = 4, EffectType = EffectTypes.DexterityBonusAmount},
                new Effect {Potency = 10, EffectType = EffectTypes.AllMeleeAttackBonusPercentage},
                new Effect {Potency = 30, EffectType = EffectTypes.PiercingBonusPercentage},
                new Effect {Potency = 15, EffectType = EffectTypes.AllMeleeDefenseBonusPercentage},
                new Effect {Potency = 15, EffectType = EffectTypes.AllElementDefenseBonusPercentage}
            };

            return new DefaultClassTemplate
            {
                Id = ClassTypes.Rogue,
                AssetCode = "class-rogue",
                NameLocaleId = "Rogue",
                DescriptionLocaleId = "Stabby Stabby",
                Effects = effects,
                Requirements = new Requirement[0]
            };
        }

        private IClassTemplate GenerateMonsterClass()
        {
            return new DefaultClassTemplate
            {
                Id = ClassTypes.Monster,
                AssetCode = "class-monster",
                NameLocaleId = "Monster",
                DescriptionLocaleId = "Monsters come in all shapes and sizes",
                Effects = new Effect[0],
                Requirements = new Requirement[0]
            };
        }
    }
}