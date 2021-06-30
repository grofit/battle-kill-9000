using System;
using System.Collections.Generic;
using BK9K.Mechanics.Loot;
using OpenRpg.Core.Requirements;
using OpenRpg.Items;
using OpenRpg.Items.Extensions;
using OpenRpg.Items.Templates;

namespace BK9K.Mechanics.Extensions
{
    public static class IItemExtensions
    {
        public static ICustomLootTableEntry GenerateCustomLootTableEntry(this IItem item, float dropRate = 1f, bool isUnique = false, IEnumerable<Requirement> requirements = null)
        {
            var variables = new DefaultLootTableEntryVariables();
            variables.DropRate(dropRate);
            variables.IsUnique(isUnique);
            return new CustomLootTableEntry()
            {
                Item = item,
                Requirements = requirements ?? Array.Empty<Requirement>(),
                Variables = variables
            };
        }
    }
}