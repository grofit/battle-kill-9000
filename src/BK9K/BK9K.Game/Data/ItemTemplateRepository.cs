using System.Collections.Generic;
using BK9K.Game.Types;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Modifications;
using OpenRpg.Core.Requirements;
using OpenRpg.Data.Defaults;
using OpenRpg.Genres.Fantasy.Types;
using OpenRpg.Items.Extensions;
using OpenRpg.Items.Templates;
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
                MakeStaff()
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
    }
}