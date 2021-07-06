

using System;
using System.Linq;
using SystemsRx.Infrastructure.Dependencies;
using SystemsRx.Infrastructure.Extensions;
using BK9K.Game.AI.Applicators.Advisories;
using BK9K.Game.AI.Applicators.Considerations.External;
using BK9K.Game.AI.Applicators.Considerations.Local;
using BK9K.Game.AI.Service;
using OpenRpg.AdviceEngine.Advisors.Applicators;
using OpenRpg.AdviceEngine.Advisors.Applicators.Registries;
using OpenRpg.AdviceEngine.Considerations.Applicators;
using OpenRpg.AdviceEngine.Considerations.Applicators.Registries;

namespace BK9K.Web.Modules
{
    public class GameAIModule : IDependencyModule
    {
        public void Setup(IDependencyContainer container)
        {
            container.Bind<IConsiderationApplicator, HasLowHealthConsiderationApplicator>();
            container.Bind<IConsiderationApplicator, IsPowerfulConsiderationApplicator>();
            container.Bind<IConsiderationApplicator, IsAbilityDamagingConsideration>();
            container.Bind<IConsiderationApplicator, IsInDangerConsideration>();
            container.Bind<IConsiderationApplicator, IsDefensiveConsiderationApplicator>();
            container.Bind<IConsiderationApplicator, EnemyDistanceConsiderationApplicator>();
            container.Bind<IConsiderationApplicator, AllyDistanceConsiderationApplicator>();
            container.Bind<IConsiderationApplicator, IsADangerConsiderationApplicator>();
            container.Bind<IConsiderationApplicator, PartyLowHealthConsiderationApplicator>();
            container.Bind<IConsiderationApplicator, EnemyLowHealthConsiderationApplicator>();
            container.Bind<IConsiderationApplicator, IsWeakConsiderationApplicator>();
            container.Bind<IConsiderationApplicator, IsVulnerableConsiderationApplicator>();
            container.Bind<IConsiderationApplicatorRegistry, DefaultConsiderationApplicatorRegistry>();
            
            container.Bind<IAdviceApplicator, ShouldUseAbilityAdviceApplicator>();
            container.Bind<IAdviceApplicator, ShouldHealOtherAdviceApplicator>();
            container.Bind<IAdviceApplicator, ShouldHealSelfAdviceApplicator>();
            container.Bind<IAdviceApplicator, ShouldEscapeAdviceApplicator>();
            container.Bind<IAdviceApplicatorRegistry, DefaultAdviceApplicatorRegistry>();

            container.Bind<IAgentService, AgentService>();
        }
    }
}