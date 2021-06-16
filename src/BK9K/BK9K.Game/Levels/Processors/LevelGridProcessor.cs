using System.Threading.Tasks;
using BK9K.Game.Processors;
using BK9K.Mechanics.Grids;

namespace BK9K.Game.Levels.Processors
{
    public class LevelGridProcessor : IProcessor<Level>
    {
        public int Priority => 2;
        
        public Task Process(Level context)
        {
            context.Grid = SetupGrid();
            return Task.CompletedTask;
        }
        
        public Grid SetupGrid()
        {
            return GridBuilder.Create()
                .WithSize(5, 5)
                .Build();
        }
    }
}