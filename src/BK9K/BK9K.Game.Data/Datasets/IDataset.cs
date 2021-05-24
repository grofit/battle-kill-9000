using System.Collections.Generic;
using OpenRpg.Cards.Effects;

namespace BK9K.Game.Data.Datasets
{
    public interface IDataset<T>
    {
        List<T> GetDataset();
    }
}