using System.Collections.Generic;
using BK9K.Game.Data.Loaders;
using OpenRpg.Core.Races;
using OpenRpg.Data.Defaults;

namespace BK9K.Game.Data.Repositories.Defaults
{
    public class RaceTemplateRepository : InMemoryDataRepository<IRaceTemplate>, IRaceTemplateRepository
    {
        public RaceTemplateRepository(IEnumerable<IRaceTemplate> data) : base(data)
        {
        }
    }
}