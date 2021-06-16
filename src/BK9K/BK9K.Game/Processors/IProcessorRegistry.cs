using System.Collections.Generic;
using System.Threading.Tasks;

namespace BK9K.Game.Processors
{
    public interface IProcessorRegistry<T>
    {
        void AddProcessor(IProcessor<T> processor);
        void RemoveProcessor(IProcessor<T> processor);
        IEnumerable<IProcessor<T>> GetProcessors();

        Task Process(T context);
    }
}