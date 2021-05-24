using BK9K.Game.Data.Datasets;
using OpenRpg.Core.Classes;
using OpenRpg.Data.Defaults;

namespace BK9K.Game.Data.Repositories.Defaults
{
    public class ClassTemplateRepository : InMemoryDataRepository<IClassTemplate>, IClassTemplateRepository
    {
        public ClassTemplateRepository()
        { Data = new ClassTemplateDataset().GetDataset(); }
    }
}