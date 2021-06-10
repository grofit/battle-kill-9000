using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using BK9K.Game.Extensions;
using BK9K.Game.Processors;
using OpenRpg.Core.Utils;

namespace BK9K.Game.Levels.Processors
{
    public class LevelUnitPlacementProcessor : IProcessor<Level>
    {
        public int Priority => 5;
        
        public IRandomizer Randomizer { get; }

        public LevelUnitPlacementProcessor(IRandomizer randomizer)
        {
            Randomizer = randomizer;
        }

        public Task Process(Level context)
        {
            foreach (var gameUnit in context.GameUnits)
            {
                var randomPosition = FindOpenPosition(context);
                gameUnit.Unit.Position = randomPosition;
            }
            return Task.CompletedTask;
        }
        
        private IEnumerable<Vector2> GeneratePosition(Level level)
        {
            while (true)
            {
                var x = Randomizer.Random(0, level.Grid.XSize-1);
                var y = Randomizer.Random(0, level.Grid.YSize-1);
                yield return new Vector2(x, y);
            }
        }

        private Vector2 FindOpenPosition(Level level)
        { return GeneratePosition(level).First(position => level.GetUnitAt(position) == null); }
        
    }
}