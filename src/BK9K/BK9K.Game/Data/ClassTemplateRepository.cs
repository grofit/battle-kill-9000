using System.Collections.Generic;
using BK9K.Framework.Types;
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
                GenerateRogueClass()
            };
        }

        private IClassTemplate GenerateFighterClass()
        {
            var effects = new[]
            {
                new Effect {Potency = 2, EffectType = EffectTypes.StrengthBonusAmount},
                new Effect {Potency = 2, EffectType = EffectTypes.ConstitutionBonusAmount},
                new Effect {Potency = 5, EffectType = EffectTypes.AllMeleeAttackBonusAmount},
                new Effect {Potency = 5, EffectType = EffectTypes.AllMeleeDefenseBonusAmount},
                new Effect {Potency = 30, EffectType = EffectTypes.HealthBonusAmount}
            };

            return new DefaultClassTemplate
            {
                Id = ClassTypes.Fighter,
                AssetCode = "class-fighter",
                NameLocaleId = "Fighter",
                DescriptionLocaleId = "Super tough, hits things",
                Effects = effects,
                Requirements = new List<Requirement>()
            };
        }

        private IClassTemplate GenerateMageClass()
        {
            var effects = new[]
            {
                new Effect {Potency = 4, EffectType = EffectTypes.IntelligenceBonusAmount},
                new Effect {Potency = 4, EffectType = EffectTypes.AllMeleeAttackBonusAmount},
                new Effect {Potency = 4, EffectType = EffectTypes.AllMeleeDefenseBonusAmount},
                new Effect {Potency = 30, EffectType = EffectTypes.MagicBonusAmount}
            };

            return new DefaultClassTemplate
            {
                Id = ClassTypes.Mage,
                AssetCode = "class-mage",
                NameLocaleId = "Mage",
                DescriptionLocaleId = "Powerful magic users",
                Effects = effects,
                Requirements = new List<Requirement>()
            };
        }
        
        private IClassTemplate GenerateRogueClass()
        {
            var effects = new[]
            {
                new Effect {Potency = 4, EffectType = EffectTypes.IntelligenceBonusAmount},
                new Effect {Potency = 4, EffectType = EffectTypes.AllMeleeAttackBonusAmount},
                new Effect {Potency = 4, EffectType = EffectTypes.AllMeleeDefenseBonusAmount},
                new Effect {Potency = 30, EffectType = EffectTypes.MagicBonusAmount}
            };

            return new DefaultClassTemplate
            {
                Id = ClassTypes.Rogue,
                AssetCode = "class-rogue",
                NameLocaleId = "Rogue",
                DescriptionLocaleId = "Stabby Stabby",
                Effects = effects,
                Requirements = new List<Requirement>()
            };
        }
    }
}