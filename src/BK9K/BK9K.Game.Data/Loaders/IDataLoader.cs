using System.Collections.Generic;

namespace BK9K.Game.Data.Loaders
{
    public interface IDataLoader<T>
    {
        IEnumerable<T> LoadData();
    }
}