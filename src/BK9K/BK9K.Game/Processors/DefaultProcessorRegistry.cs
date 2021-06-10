using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BK9K.Game.Processors
{
    public class DefaultProcessorRegistry<T> : IProcessorRegistry<T>
    {
        public List<IProcessor<T>> Processors { get; }

        public DefaultProcessorRegistry(IEnumerable<IProcessor<T>> processors = null)
        {
            Processors = processors?.ToList() ?? new List<IProcessor<T>>();
        }

        public IEnumerable<IProcessor<T>> GetProcessors() => Processors;
        public void AddProcessor(IProcessor<T> processor) => Processors.Add(processor);
        public void RemoveProcessor(IProcessor<T> processor) => Processors.Remove(processor);

        public async Task Process(T context)
        {
            var orderedProcessors = Processors.OrderBy(x => x.Priority);
            foreach (var processor in orderedProcessors)
            { await processor.Process(context); }
        }
    }
}