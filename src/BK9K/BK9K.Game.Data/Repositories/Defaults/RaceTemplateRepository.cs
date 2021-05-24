using BK9K.Game.Data.Datasets;
using OpenRpg.Core.Races;
using OpenRpg.Data.Defaults;

namespace BK9K.Game.Data.Repositories.Defaults
{
    public class RaceTemplateRepository : InMemoryDataRepository<IRaceTemplate>, IRaceTemplateRepository
    {
        public RaceTemplateRepository()
        {
            Data = new RaceTemplateDataset().GetDataset();
        }
    }
}