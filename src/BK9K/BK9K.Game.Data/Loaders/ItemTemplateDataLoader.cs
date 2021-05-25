using System.Collections.Generic;
using BK9K.Mechanics.Extensions;
using BK9K.Mechanics.Types.Lookups;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Modifications;
using OpenRpg.Core.Requirements;
using OpenRpg.Genres.Fantasy.Types;
using OpenRpg.Items.Extensions;
using OpenRpg.Items.Templates;

namespace BK9K.Game.Data.Loaders
{
    public class ItemTemplateDataLoader : IDataLoader<IItemTemplate>
    {
        public IEnumerable<IItemTemplate> LoadData()
        {
            return new List<IItemTemplate>
            {
                MakeSword(),
                MakeDagger(),
                MakeStaff(),
                MakePlateArmour(),
                MakeRobe(),
                MakeTunic(),
                MakeDeadlySosig(),
                MakeMinorHealthPotion(),
                MakeMajorHealthPotion()
            };
        }

        private IItemTemplate MakeSword()
        {
            var template = new DefaultItemTemplate
            {
                Id = ItemTemplateLookups.Sword,
                NameLocaleId = "Sword",
                DescriptionLocaleId = "A really bad looking sword, can slay things though",
                ItemType = ItemTypes.GenericWeapon,
                Effects = new[]
                {
                    new Effect { EffectType = EffectTypes.SlashingDamageAmount, Potency = 2.0f }
                }
            };
            template.Variables.QualityType(ItemQualityTypes.CommonQuality);
            template.Variables.Value(10);
            template.Variables.AssetCode("sword");
            return template;
        }

        private IItemTemplate MakeDagger()
        {
            var template = new DefaultItemTemplate
            {
                Id = ItemTemplateLookups.Dagger,
                NameLocaleId = "Dagger",
                DescriptionLocaleId = "A pretty sweet dagger",
                ItemType = ItemTypes.GenericWeapon,
                Effects = new[]
                {
                    new Effect { EffectType = EffectTypes.PiercingDamageAmount, Potency = 2.0f }
                }
            };
            template.Variables.QualityType(ItemQualityTypes.CommonQuality);
            template.Variables.Value(10);
            template.Variables.AssetCode("dagger");
            return template;
        }

        private IItemTemplate MakeStaff()
        {
            var template = new DefaultItemTemplate
            {
                Id = ItemTemplateLookups.Staff,
                NameLocaleId = "Staff",
                DescriptionLocaleId = "A staff with magic runes on it",
                ItemType = ItemTypes.GenericWeapon,
                Effects = new[]
                {
                    new Effect { EffectType = EffectTypes.FireBonusAmount, Potency = 2.0f }
                }
            };
            template.Variables.QualityType(ItemQualityTypes.CommonQuality);
            template.Variables.Value(10);
            template.Variables.AssetCode("staff");
            return template;
        }

        private IItemTemplate MakePlateArmour()
        {
            var template = new DefaultItemTemplate
            {
                Id = ItemTemplateLookups.PlateArmour,
                NameLocaleId = "Plate Armour",
                DescriptionLocaleId = "A metallic suit of armour",
                ItemType = ItemTypes.UpperBodyArmour,
                Effects = new[]
                {
                    new Effect { EffectType = EffectTypes.AllMeleeDefenseBonusAmount, Potency = 2.0f }
                }
            };
            template.Variables.QualityType(ItemQualityTypes.CommonQuality);
            template.Variables.Value(10);
            template.Variables.AssetCode("plate-armour");
            return template;
        }

        private IItemTemplate MakeRobe()
        {
            var template = new DefaultItemTemplate
            {
                Id = ItemTemplateLookups.Robe,
                NameLocaleId = "Robe",
                DescriptionLocaleId = "A cloth robe with runes",
                ItemType = ItemTypes.UpperBodyArmour,
                Effects = new[]
                {
                    new Effect { EffectType = EffectTypes.AllMeleeDefenseBonusAmount, Potency = 0.5f },
                    new Effect { EffectType = EffectTypes.AllElementDefenseBonusAmount, Potency = 1.0f }
                }
            };
            template.Variables.QualityType(ItemQualityTypes.CommonQuality);
            template.Variables.Value(10);
            template.Variables.AssetCode("robe");
            return template;
        }

        private IItemTemplate MakeTunic()
        {
            var template = new DefaultItemTemplate
            {
                Id = ItemTemplateLookups.Tunic,
                NameLocaleId = "Tunic",
                DescriptionLocaleId = "A leather Tunic",
                ItemType = ItemTypes.UpperBodyArmour,
                Effects = new[]
                {
                    new Effect { EffectType = EffectTypes.AllMeleeDefenseBonusAmount, Potency = 1.0f },
                    new Effect { EffectType = EffectTypes.DexterityBonusAmount, Potency = 1.0f }
                }
            };
            template.Variables.QualityType(ItemQualityTypes.CommonQuality);
            template.Variables.Value(10);
            template.Variables.AssetCode("tunic");
            return template;
        }

        private IItemTemplate MakeDeadlySosig()
        {
            var template = new DefaultItemTemplate
            {
                Id = ItemTemplateLookups.DeadlySosig,
                NameLocaleId = "The Deadly Sosig",
                DescriptionLocaleId = "Its a sosig",
                ItemType = ItemTypes.GenericWeapon,
                Effects = new[]
                {
                    new Effect { EffectType = EffectTypes.PiercingBonusAmount, Potency = 5.0f },
                    new Effect { EffectType = EffectTypes.DexterityBonusAmount, Potency = 5.0f }
                }
            };
            template.Variables.QualityType(ItemQualityTypes.CommonQuality);
            template.Variables.Value(10);
            template.Variables.AssetCode("the-deadly-sosig");
            return template;
        }

        private IItemTemplate MakeMinorHealthPotion()
        {
            var template = new DefaultItemTemplate
            {
                Id = ItemTemplateLookups.MinorHealthPotion,
                NameLocaleId = "Minor Heal Potion",
                DescriptionLocaleId = "This potion can heal a small amount of Health to the consumer",
                ItemType = ItemTypes.Potions,
                ModificationAllowances = new ModificationAllowance[0],
                Requirements = new Requirement[0],
                Effects = new[]
                {
                    new Effect { EffectType = EffectTypes.HealthRestoreAmount, Potency = 20.0f }
                }
            };
            template.Variables.QualityType(ItemQualityTypes.CommonQuality);
            template.Variables.Value(10);
            template.Variables.AssetCode("minor-heal-potion");
            return template;
        }

        private IItemTemplate MakeMajorHealthPotion()
        {
            var template = new DefaultItemTemplate
            {
                Id = ItemTemplateLookups.MajorHealthPotion,
                NameLocaleId = "Major Heal Potion",
                DescriptionLocaleId = "This potion can heal a large amount of Health to the consumer",
                ItemType = ItemTypes.Potions,
                Effects = new[]
                {
                    new Effect { EffectType = EffectTypes.HealthRestoreAmount, Potency = 50.0f }
                }
            };
            template.Variables.QualityType(ItemQualityTypes.CommonQuality);
            template.Variables.Value(10);
            template.Variables.AssetCode("major-heal-potion");
            return template;
        }
    }
}