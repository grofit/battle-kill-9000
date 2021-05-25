using System.Collections.Generic;
using OpenRpg.Cards.Effects;
using OpenRpg.Data.Defaults;

namespace BK9K.Game.Data.Repositories.Defaults
{
    public class CardEffectsRepository : InMemoryDataRepository<CardEffects>, ICardEffectsRepository
    {
        public CardEffectsRepository(IEnumerable<CardEffects> data) : base(data)
        {}
    }
}