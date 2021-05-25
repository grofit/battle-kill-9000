using System.Collections.Generic;
using BK9K.Game.Data.Loaders;
using OpenRpg.Data.Defaults;
using OpenRpg.Items.Templates;

namespace BK9K.Game.Data.Repositories.Defaults
{
    public class ItemTemplateRepository : InMemoryDataRepository<IItemTemplate>, IItemTemplateRepository
    {
        public ItemTemplateRepository(IEnumerable<IItemTemplate> data) : base(data)
        {
        }
    }
}