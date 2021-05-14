using BK9K.Framework.Conventions;
using OpenRpg.Core.Common;
using OpenRpg.Core.Effects;

namespace BK9K.Framework.Cards
{
    public interface ICard : IHasLocaleDescription, IHasEffects, IHasUniqueId
    {
        int CardType { get; }
    }
}