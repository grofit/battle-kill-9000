using BK9K.Game.Data.Datasets;
using OpenRpg.Data.Defaults;
using OpenRpg.Items.Templates;

namespace BK9K.Game.Data.Repositories.Defaults
{
    public class ItemTemplateRepository : InMemoryDataRepository<IItemTemplate>, IItemTemplateRepository
    {
        public ItemTemplateRepository()
        {
            Data = new ItemTemplateDataset().GetDataset();
        }
    }
}