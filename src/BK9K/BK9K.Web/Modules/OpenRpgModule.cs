using System;
using SystemsRx.Infrastructure.Dependencies;
using SystemsRx.Infrastructure.Extensions;
using BK9K.Framework.Random;
using BK9K.Game.Data;
using OpenRpg.Combat.Processors;
using OpenRpg.Core.Stats;
using OpenRpg.Core.Utils;
using OpenRpg.Genres.Fantasy.Combat;
using OpenRpg.Genres.Fantasy.Defaults;
using OpenRpg.Genres.Fantasy.Requirements;
using OpenRpg.Genres.Fantasy.Stats;
using OpenRpg.Localization.Repositories;

namespace BK9K.Web.Modules
{
    public class OpenRpgModule : IDependencyModule
    {
        public void Setup(IDependencyContainer container)
        {
            container.Bind<IAttributeStatPopulator, DefaultAttributeStatPopulator>();
            container.Bind<IVitalStatsPopulator, DefaultVitalStatsPopulator>();
            container.Bind<IDamageStatPopulator, DefaultDamageStatPopulator>();
            container.Bind<IDefenseStatPopulator, DefaultDefenseStatPopulator>();
            container.Bind<IStatsComputer, DefaultStatsComputer>();
            container.Bind<IRandomizer>(x =>  x.ToInstance(new DefaultRandomizer(new Random())));
            container.Bind<IAttackGenerator, BasicAttackGenerator>();
            container.Bind<IAttackProcessor, DefaultAttackProcessor>();
            container.Bind<IRequirementChecker, DefaultRequirementChecker>();
            container.Bind<ILocaleRepository, DefaultLocaleRepository>();
            
        }
    }
}