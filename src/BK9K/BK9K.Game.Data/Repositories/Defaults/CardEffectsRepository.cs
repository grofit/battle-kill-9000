using BK9K.Game.Data.Datasets;
using OpenRpg.Cards.Effects;
using OpenRpg.Data.Defaults;

namespace BK9K.Game.Data.Repositories.Defaults
{
    public class CardEffectsRepository : InMemoryDataRepository<CardEffects>, ICardEffectsRepository
    {
        public CardEffectsRepository()
        { Data = new CardEffectsDataset().GetDataset(); }
    }
}