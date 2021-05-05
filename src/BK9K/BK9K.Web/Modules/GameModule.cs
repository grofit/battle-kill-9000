using BK9K.Framework.Grids;
using BK9K.Game;
using BK9K.Game.Builders;
using BK9K.Game.Configuration;
using BK9K.Game.Data;
using EcsRx.Infrastructure.Dependencies;
using EcsRx.Infrastructure.Extensions;

namespace BK9K.Web.Modules
{
    public class GameModule : IDependencyModule
    {
        public void Setup(IDependencyContainer container)
        {
            container.Bind<GridBuilder>();
            container.Bind<UnitBuilder>();
            container.Bind<World>();
            container.Bind<GameConfiguration>();
        }
    }
}