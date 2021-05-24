using System.Collections.Generic;
using SystemsRx.Infrastructure.Dependencies;
using SystemsRx.Infrastructure.Extensions;
using BK9K.Game.Data.Datasets;
using BK9K.Game.Data.Repositories;
using BK9K.Game.Data.Repositories.Defaults;
using OpenRpg.Cards.Effects;
using OpenRpg.Combat.Abilities;
using OpenRpg.Combat.Effects;
using OpenRpg.Core.Classes;
using OpenRpg.Core.Races;
using OpenRpg.Items.Templates;

namespace BK9K.Web.Modules
{
    public class GameDataModule : IDependencyModule
    {
        public void Setup(IDependencyContainer container)
        {
            container.Bind<IAbilityHandlerRepository, AbilityHandlerRepository>();
            container.Bind<ISpellHandlerRepository, SpellHandlerRepository>();
            container.Bind<ICardEffectsRepository, CardEffectsRepository>();
            container.Bind<IRaceTemplateRepository, RaceTemplateRepository>();
            container.Bind<IClassTemplateRepository, ClassTemplateRepository>();
            container.Bind<IItemTemplateRepository, ItemTemplateRepository>();
            container.Bind<IAbilityRepository, AbilityRepository>();
            container.Bind<ITimedEffectRepository, TimedEffectRepository>();
            container.Bind<ISpellRepository, SpellRepository>();
        }
    }
}