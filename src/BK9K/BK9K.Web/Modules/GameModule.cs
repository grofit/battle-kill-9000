using BK9K.Framework.Grids;
using BK9K.Framework.Units;
using BK9K.Game;
using BK9K.Game.Builders;
using BK9K.Game.Data;
using BK9K.Web.Infrastructure.DI;
using Microsoft.Extensions.DependencyInjection;

namespace BK9K.Web.Modules
{
    public class GameModule : IModule
    {
        public void Setup(IServiceCollection services)
        {
            services.AddSingleton<GridBuilder>();
            services.AddSingleton<RaceRepository>();
            services.AddSingleton<ClassRepository>();
            services.AddSingleton<UnitBuilder>();
            services.AddSingleton<World>();
        }
    }
}