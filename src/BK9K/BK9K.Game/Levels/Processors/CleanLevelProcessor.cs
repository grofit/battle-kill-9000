using System.Threading.Tasks;
using BK9K.Game.Pools;
using BK9K.Game.Processors;

namespace BK9K.Game.Levels.Processors
{
    public class CleanLevelProcessor : IProcessor<Level>
    {
        public int Priority => 1;
        
        public IUnitIdPool UnitIdPool { get; }

        public CleanLevelProcessor(IUnitIdPool unitIdPool)
        {
            UnitIdPool = unitIdPool;
        }

        public Task Process(Level context)
        {
            DisposeExistingData(context);
            return Task.CompletedTask;
        }
        
        public void DisposeExistingData(Level context)
        {
            if (context.GameUnits.Count != 0)
            { 
                context.GameUnits.ForEach(x =>
                {
                    x.Agent.Dispose();
                    UnitIdPool.ReleaseInstance(x.Unit.Id);
                }); 
            }
            
            context.GameUnits.Clear();
        }
    }
}