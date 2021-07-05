using System.Collections.Generic;
using BK9K.Mechanics.Classes;
using OpenRpg.Core.Classes;
using OpenRpg.Data.Defaults;

namespace BK9K.Game.Data.Repositories.Defaults
{
    public class ClassTemplateRepository : InMemoryDataRepository<ICustomClassTemplate>, IClassTemplateRepository
    {
        public ClassTemplateRepository(IEnumerable<ICustomClassTemplate> data) : base(data)
        {}
    }
}