using System.Collections.Generic;
using SystemsRx.Events;
using SystemsRx.Infrastructure.Dependencies;
using SystemsRx.Infrastructure.Extensions;
using BK9K.Game.Data.Loaders;
using BK9K.Game.Data.Repositories;
using BK9K.Game.Data.Repositories.Defaults;
using BK9K.Web.Debug;
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
            container.Bind<ICardEffectsRepository, CardEffectsRepository>(x => x.WithConstructorArg(new CardEffectsDataLoader().LoadData()));
            container.Bind<IRaceTemplateRepository, RaceTemplateRepository>(x => x.WithConstructorArg(new RaceTemplateDataLoader().LoadData()));
            container.Bind<IClassTemplateRepository, ClassTemplateRepository>(x => x.WithConstructorArg(new ClassTemplateDataLoader().LoadData()));
            container.Bind<IItemTemplateRepository, ItemTemplateRepository>(x => x.WithConstructorArg(new ItemTemplateDataLoader().LoadData()));
            container.Bind<IAbilityRepository, AbilityRepository>(x => x.WithConstructorArg(new AbilityDataLoader().LoadData()));
            container.Bind<ITimedEffectRepository, TimedEffectRepository>(x => x.WithConstructorArg(new TimedEffectDataLoader().LoadData()));
            container.Bind<ISpellRepository, SpellRepository>();
        }
    }
}