using OpenRpg.Data.Repositories;
using OpenRpg.Items.Templates;

namespace BK9K.Game.Data.Repositories
{
    public interface IItemTemplateRepository : IReadRepository<IItemTemplate, int>
    {
    }
}