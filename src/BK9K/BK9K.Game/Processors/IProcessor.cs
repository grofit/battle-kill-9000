using System.Threading.Tasks;

namespace BK9K.Game.Processors
{
    public interface IProcessor<in T>
    {
        /// <summary>
        /// The priority will go from low to high, so 1 first then 2 after etc
        /// </summary>
        int Priority { get; }
        
        Task Process(T context);
    }
}