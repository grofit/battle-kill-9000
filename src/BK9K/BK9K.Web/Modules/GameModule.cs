using SystemsRx.Infrastructure.Dependencies;
using SystemsRx.Infrastructure.Extensions;
using BK9K.Framework.Grids;
using BK9K.Game;
using BK9K.Game.Builders;
using BK9K.Game.Configuration;

namespace BK9K.Web.Modules
{
    public class GameModule : IDependencyModule
    {
        public void Setup(IDependencyContainer container)
        {
            container.Bind<GridBuilder>();
            container.Bind<UnitBuilder>();
            container.Bind<Level>();
            container.Bind<GameConfiguration>();
            container.Bind<GameState>();
        }
    }
}