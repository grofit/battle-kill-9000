using System.Collections.Generic;
using BK9K.Mechanics.Classes;
using BK9K.Mechanics.Extensions;
using BK9K.Mechanics.Types.Lookups;
using OpenRpg.Core.Effects;
using OpenRpg.Genres.Fantasy.Types;

namespace BK9K.Game.Data.Loaders
{
    public class ClassTemplateDataLoader : IDataLoader<ICustomClassTemplate>
    {
        public IEnumerable<ICustomClassTemplate> LoadData()
        {
            return new List<ICustomClassTemplate>
            {
                GenerateFighterClass(),
                GenerateMageClass(),
                GenerateRogueClass(),
                GenerateMonsterClass(),
                GeneratePriestClass()
            };
        }

        private ICustomClassTemplate GenerateFighterClass()
        {
            var effects = new[]
            {
                new Effect {Potency = 2, EffectType = EffectTypes.StrengthBonusAmount},
                new Effect {Potency = 2, EffectType = EffectTypes.ConstitutionBonusAmount},
                new Effect {Potency = 20, EffectType = EffectTypes.AllMeleeAttackBonusPercentage},
                new Effect {Potency = 30, EffectType = EffectTypes.AllMeleeDefenseBonusPercentage},
                new Effect {Potency = 10, EffectType = EffectTypes.HealthBonusAmount}
            };
            
            var levelUpEffects = new[]
            {
                new Effect {Potency = 1, EffectType = EffectTypes.StrengthBonusAmount},
                new Effect {Potency = 1, EffectType = EffectTypes.ConstitutionBonusAmount},
                new Effect {Potency = 1, EffectType = EffectTypes.AllMeleeAttackBonusAmount},
                new Effect {Potency = 1, EffectType = EffectTypes.AllMeleeDefenseBonusPercentage},
                new Effect {Potency = 6, EffectType = EffectTypes.HealthBonusAmount}
            };

            var template = new CustomClassTemplate
            {
                Id = ClassLookups.Fighter,
                NameLocaleId = "Fighter",
                DescriptionLocaleId = "Super tough, hits things",
                Effects = effects,
                LevelUpEffects = levelUpEffects
            };
            template.Variables.AssetCode("class-fighter");

            return template;
        }

        private ICustomClassTemplate GenerateMageClass()
        {
            var effects = new[]
            {
                new Effect {Potency = 4, EffectType = EffectTypes.IntelligenceBonusAmount},
                new Effect {Potency = 4, EffectType = EffectTypes.AllMeleeAttackBonusPercentage},
                new Effect {Potency = 30, EffectType = EffectTypes.AllElementDefenseBonusPercentage},
                new Effect {Potency = 30, EffectType = EffectTypes.MagicBonusPercentage}
            };
            
            var levelUpEffects = new[]
            {
                new Effect {Potency = 1, EffectType = EffectTypes.IntelligenceBonusAmount},
                new Effect {Potency = 10, EffectType = EffectTypes.MagicBonusPercentage},
                new Effect {Potency = 2, EffectType = EffectTypes.HealthBonusAmount}
            };

            var template = new CustomClassTemplate
            {
                Id = ClassLookups.Mage,
                NameLocaleId = "Mage",
                DescriptionLocaleId = "Powerful magic users",
                Effects = effects,
                LevelUpEffects = levelUpEffects
            };
            template.Variables.AssetCode("class-mage");
            return template;
        }

        private ICustomClassTemplate GenerateRogueClass()
        {
            var effects = new[]
            {
                new Effect {Potency = 4, EffectType = EffectTypes.DexterityBonusAmount},
                new Effect {Potency = 10, EffectType = EffectTypes.AllMeleeAttackBonusPercentage},
                new Effect {Potency = 30, EffectType = EffectTypes.PiercingBonusPercentage},
                new Effect {Potency = 15, EffectType = EffectTypes.AllMeleeDefenseBonusPercentage},
                new Effect {Potency = 15, EffectType = EffectTypes.AllElementDefenseBonusPercentage}
            };

            var levelUpEffects = new[]
            {
                new Effect {Potency = 1, EffectType = EffectTypes.DexterityBonusAmount},
                new Effect {Potency = 10, EffectType = EffectTypes.PiercingBonusPercentage},
                new Effect {Potency = 2, EffectType = EffectTypes.PiercingBonusPercentage},
                new Effect {Potency = 4, EffectType = EffectTypes.HealthBonusAmount}
            };

            var template = new CustomClassTemplate
            {
                Id = ClassLookups.Rogue,
                NameLocaleId = "Rogue",
                DescriptionLocaleId = "Stabby Stabby",
                Effects = effects,
                LevelUpEffects = levelUpEffects
            };
            template.Variables.AssetCode("class-mage");
            return template;
        }
        
        private ICustomClassTemplate GeneratePriestClass()
        {
            var effects = new[]
            {
                new Effect {Potency = 4, EffectType = EffectTypes.WisdomBonusAmount},
                new Effect {Potency = 4, EffectType = EffectTypes.AllMeleeAttackBonusPercentage},
                new Effect {Potency = 30, EffectType = EffectTypes.AllElementDefenseBonusPercentage},
                new Effect {Potency = 30, EffectType = EffectTypes.MagicBonusPercentage}
            };
            
            var levelUpEffects = new[]
            {
                new Effect {Potency = 2, EffectType = EffectTypes.WisdomBonusAmount},
                new Effect {Potency = 10, EffectType = EffectTypes.AllElementDefenseBonusPercentage},
                new Effect {Potency = 1, EffectType = EffectTypes.AllElementDefenseBonusAmount},
                new Effect {Potency = 2, EffectType = EffectTypes.HealthBonusAmount}
            };

            var template = new CustomClassTemplate
            {
                Id = ClassLookups.Priest,
                NameLocaleId = "Priest",
                DescriptionLocaleId = "Priests wield holy light to heal other units",
                Effects = effects,
                LevelUpEffects = levelUpEffects
            };
            template.Variables.AssetCode("class-priest");
            return template;
        }

        private ICustomClassTemplate GenerateMonsterClass()
        {
            var template = new CustomClassTemplate()
            {
                Id = ClassLookups.Monster,
                NameLocaleId = "Monster",
                DescriptionLocaleId = "Monsters come in all shapes and sizes",
                Effects = new Effect[0]
            };
            template.Variables.AssetCode("class-monster");
            return template;
        }
    }
}