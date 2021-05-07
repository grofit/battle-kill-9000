using OpenRpg.Core.Common;
using OpenRpg.Core.Effects;
using OpenRpg.Localization;

namespace BK9K.Framework.Cards
{
    public interface ICard : IHasLocaleDescription, IHasEffects
    {
        int CardType { get; }
    }
}