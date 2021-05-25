﻿using System.Collections.Generic;
using BK9K.Mechanics.Extensions;
using BK9K.Mechanics.Types.Lookups;
using OpenRpg.Core.Classes;
using OpenRpg.Core.Effects;
using OpenRpg.Genres.Fantasy.Types;

namespace BK9K.Game.Data.Loaders
{
    public class ClassTemplateDataLoader : IDataLoader<IClassTemplate>
    {
        public IEnumerable<IClassTemplate> LoadData()
        {
            return new List<IClassTemplate>
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

            var template = new DefaultClassTemplate
            {
                Id = ClassLookups.Fighter,
                NameLocaleId = "Fighter",
                DescriptionLocaleId = "Super tough, hits things",
                Effects = effects
            };
            template.Variables.AssetCode("class-fighter");

            return template;
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

            var template = new DefaultClassTemplate
            {
                Id = ClassLookups.Mage,
                NameLocaleId = "Mage",
                DescriptionLocaleId = "Powerful magic users",
                Effects = effects
            };
            template.Variables.AssetCode("class-mage");
            return template;
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

            var template = new DefaultClassTemplate
            {
                Id = ClassLookups.Rogue,
                NameLocaleId = "Rogue",
                DescriptionLocaleId = "Stabby Stabby",
                Effects = effects
            };
            template.Variables.AssetCode("class-mage");
            return template;
        }

        private IClassTemplate GenerateMonsterClass()
        {
            var template = new DefaultClassTemplate
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