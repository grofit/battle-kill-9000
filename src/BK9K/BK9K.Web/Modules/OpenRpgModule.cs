using System;
using BK9K.Framework.Random;
using BK9K.Web.Infrastructure.DI;
using Microsoft.Extensions.DependencyInjection;
using OpenRpg.Combat.Processors;
using OpenRpg.Core.Stats;
using OpenRpg.Core.Utils;
using OpenRpg.Genres.Fantasy.Combat;
using OpenRpg.Genres.Fantasy.Defaults;
using OpenRpg.Genres.Fantasy.Requirements;
using OpenRpg.Genres.Fantasy.Stats;

namespace BK9K.Web.Modules
{
    public class OpenRpgModule : IModule
    {
        public void Setup(IServiceCollection services)
        {
            services.AddSingleton<IAttributeStatPopulator, DefaultAttributeStatPopulator>();
            services.AddSingleton<IVitalStatsPopulator, DefaultVitalStatsPopulator>();
            services.AddSingleton<IDamageStatPopulator, DefaultDamageStatPopulator>();
            services.AddSingleton<IDefenseStatPopulator, DefaultDefenseStatPopulator>();
            services.AddSingleton<IStatsComputer, DefaultStatsComputer>();
            services.AddSingleton<IRandomizer>(x => new DefaultRandomizer(new Random()));
            services.AddSingleton<IAttackGenerator, BasicAttackGenerator>();
            services.AddSingleton<IAttackProcessor, DefaultAttackProcessor>();
            services.AddSingleton<IRequirementChecker, DefaultRequirementChecker>();
        }
    }
}