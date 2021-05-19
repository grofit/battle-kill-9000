using System.Collections.Generic;
using OpenRpg.Core.Requirements;
using OpenRpg.Items;
using OpenRpg.Items.Loot;
using OpenRpg.Items.Templates;

namespace BK9K.Framework.Loot
{
    public class LootTableEntry : ILootTableEntry
    {
        public IEnumerable<Requirement> Requirements { get; set; }
        public ILootTableEntryVariables Variables { get; set; }
        public IItem Item { get; set; }
    }
}