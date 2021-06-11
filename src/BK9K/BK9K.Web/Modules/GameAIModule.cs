

using SystemsRx.Infrastructure.Dependencies;
using SystemsRx.Infrastructure.Extensions;
using BK9K.Game.AI.Applicators.Advisories;
using BK9K.Game.AI.Applicators.Considerations.External;
using BK9K.Game.AI.Applicators.Considerations.Local;
using BK9K.UAI.Advisors.Applicators;
using BK9K.UAI.Advisors.Applicators.Registries;
using BK9K.UAI.Considerations.Applicators;
using BK9K.UAI.Considerations.Applicators.Registries;

namespace BK9K.Web.Modules
{
    public class GameAIModule : IDependencyModule
    {
        public void Setup(IDependencyContainer container)
        {
            container.Bind<IConsiderationApplicator, NeedsHealingConsiderationApplicator>();
            container.Bind<IConsiderationApplicator, IsPowerfulConsiderationApplicator>();
            container.Bind<IConsiderationApplicator, IsInDangerConsideration>();
            container.Bind<IConsiderationApplicator, IsDefensiveConsiderationApplicator>();
            container.Bind<IConsiderationApplicator, EnemyDistanceConsiderationApplicator>();
            container.Bind<IConsiderationApplicator, AllyDistanceConsiderationApplicator>();
            container.Bind<IConsiderationApplicator, IsADangerConsiderationApplicator>();
            container.Bind<IConsiderationApplicator, PartyNeedsHealingConsiderationApplicator>();
            container.Bind<IConsiderationApplicator, EnemyLowHealthConsiderationApplicator>();
            container.Bind<IConsiderationApplicatorRegistry, DefaultConsiderationApplicatorRegistry>();
            
            container.Bind<IAdviceApplicator, ShouldAttackAdviceApplicator>();
            container.Bind<IAdviceApplicator, ShouldHealOtherAdviceApplicator>();
            container.Bind<IAdviceApplicator, ShouldHealSelfAdviceApplicator>();
            container.Bind<IAdviceApplicatorRegistry, DefaultAdviceApplicatorRegistry>();
        }
    }
}