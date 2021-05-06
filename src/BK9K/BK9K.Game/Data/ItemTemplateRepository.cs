using BK9K.Game.Types;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Modifications;
using OpenRpg.Core.Requirements;
using OpenRpg.Data.Defaults;
using OpenRpg.Genres.Fantasy.Types;
using OpenRpg.Items.Extensions;
using OpenRpg.Items.Templates;
using System.Collections.Generic;
using ItemTypes = OpenRpg.Genres.Fantasy.Types.ItemTypes;

namespace BK9K.Game.Data
{
    public class ItemTemplateRepository : InMemoryDataRepository<IItemTemplate>, IItemTemplateRepository
    {
        public ItemTemplateRepository()
        {
            Data = new List<IItemTemplate>
            {
                MakeSword(),
                MakeDagger(),
                MakeStaff(),
                MakePlateArmour(),
                MakeRobe(),
                MakeTunic()
            };
        }

        private IItemTemplate MakeSword()
        {
            var template = new DefaultItemTemplate
            {
                Id = ItemTemplateLookups.Sword,
                NameLocaleId = "Sword",
                AssetCode = "sword",
                DescriptionLocaleId = "A really bad looking sword, can slay things though",
                ItemType = ItemTypes.GenericWeapon,
                ModificationAllowances = new ModificationAllowance[0],
                Requirements = new Requirement[0],
                Effects = new[]
                {
                    new Effect { EffectType = EffectTypes.SlashingDamageAmount, Potency = 2.0f }
                }
            };
            template.Variables.QualityType(ItemQualityTypes.CommonQuality);
            template.Variables.Value(10);
            return template;
        }

        private IItemTemplate MakeDagger()
        {
            var template = new DefaultItemTemplate
            {
                Id = ItemTemplateLookups.Dagger,
                NameLocaleId = "Dagger",
                AssetCode = "dagger",
                DescriptionLocaleId = "A pretty sweet dagger",
                ItemType = ItemTypes.GenericWeapon,
                ModificationAllowances = new ModificationAllowance[0],
                Requirements = new Requirement[0],
                Effects = new[]
                {
                    new Effect { EffectType = EffectTypes.PiercingDamageAmount, Potency = 2.0f }
                }
            };
            template.Variables.QualityType(ItemQualityTypes.CommonQuality);
            template.Variables.Value(10);
            return template;
        }

        private IItemTemplate MakeStaff()
        {
            var template = new DefaultItemTemplate
            {
                Id = ItemTemplateLookups.Staff,
                NameLocaleId = "Staff",
                AssetCode = "staff",
                DescriptionLocaleId = "A staff with magic runes on it",
                ItemType = ItemTypes.GenericWeapon,
                ModificationAllowances = new ModificationAllowance[0],
                Requirements = new Requirement[0],
                Effects = new[]
                {
                    new Effect { EffectType = EffectTypes.FireBonusAmount, Potency = 2.0f }
                }
            };
            template.Variables.QualityType(ItemQualityTypes.CommonQuality);
            template.Variables.Value(10);
            return template;
        }

        private IItemTemplate MakePlateArmour()
        {
            var template = new DefaultItemTemplate
            {
                Id = ItemTemplateLookups.PlateArmour,
                NameLocaleId = "Plate Armour",
                AssetCode = "plate-armour",
                DescriptionLocaleId = "A metallic suit of armour",
                ItemType = ItemTypes.UpperBodyArmour,
                ModificationAllowances = new ModificationAllowance[0],
                Requirements = new Requirement[0],
                Effects = new[]
                {
                    new Effect { EffectType = EffectTypes.AllMeleeDefenseBonusAmount, Potency = 2.0f }
                }
            };
            template.Variables.QualityType(ItemQualityTypes.CommonQuality);
            template.Variables.Value(10);
            return template;
        }

        private IItemTemplate MakeRobe()
        {
            var template = new DefaultItemTemplate
            {
                Id = ItemTemplateLookups.Robe,
                NameLocaleId = "Robe",
                AssetCode = "robe",
                DescriptionLocaleId = "A cloth robe with runes",
                ItemType = ItemTypes.UpperBodyArmour,
                ModificationAllowances = new ModificationAllowance[0],
                Requirements = new Requirement[0],
                Effects = new[]
                {
                    new Effect { EffectType = EffectTypes.AllMeleeDefenseBonusAmount, Potency = 0.5f },
                    new Effect { EffectType = EffectTypes.AllElementDefenseBonusAmount, Potency = 1.0f }
                }
            };
            template.Variables.QualityType(ItemQualityTypes.CommonQuality);
            template.Variables.Value(10);
            return template;
        }

        private IItemTemplate MakeTunic()
        {
            var template = new DefaultItemTemplate
            {
                Id = ItemTemplateLookups.Tunic,
                NameLocaleId = "Tunic",
                AssetCode = "tunic",
                DescriptionLocaleId = "A leather Tunic",
                ItemType = ItemTypes.UpperBodyArmour,
                ModificationAllowances = new ModificationAllowance[0],
                Requirements = new Requirement[0],
                Effects = new[]
                {
                    new Effect { EffectType = EffectTypes.AllMeleeDefenseBonusAmount, Potency = 1.0f },
                    new Effect { EffectType = EffectTypes.DexterityBonusAmount, Potency = 1.0f }
                }
            };
            template.Variables.QualityType(ItemQualityTypes.CommonQuality);
            template.Variables.Value(10);
            return template;
        }
    }
}