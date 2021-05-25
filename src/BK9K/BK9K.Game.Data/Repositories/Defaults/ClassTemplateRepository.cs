using System.Collections.Generic;
using OpenRpg.Core.Classes;
using OpenRpg.Data.Defaults;

namespace BK9K.Game.Data.Repositories.Defaults
{
    public class ClassTemplateRepository : InMemoryDataRepository<IClassTemplate>, IClassTemplateRepository
    {
        public ClassTemplateRepository(IEnumerable<IClassTemplate> data) : base(data)
        {}
    }
}