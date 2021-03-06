using BK9K.Game.Data.Repositories.Defaults;
using OpenRpg.Localization.Repositories;

namespace BK9K.Game.Data.Extensions
{
    public static class ILocaleRepositoryExtensions
    {
        public static string GetKeyFor(this ILocaleRepository repository, string typeKey, int typeValue)
        { return DefaultLocaleRepository.GetKeyFor(typeKey, typeValue); }
    }
}