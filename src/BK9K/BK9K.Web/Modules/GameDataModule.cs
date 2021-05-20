using SystemsRx.Infrastructure.Dependencies;
using SystemsRx.Infrastructure.Extensions;
using BK9K.Game.Data;

namespace BK9K.Web.Modules
{
    public class GameDataModule : IDependencyModule
    {
        public void Setup(IDependencyContainer container)
        {
            container.Bind<IAbilityHandlerRepository, AbilityHandlerRepository>();
            container.Bind<ICardEffectsRepository, CardEffectsRepository>();
            container.Bind<IRaceTemplateRepository, RaceTemplateRepository>();
            container.Bind<IClassTemplateRepository, ClassTemplateRepository>();
            container.Bind<IItemTemplateRepository, ItemTemplateRepository>();
            container.Bind<IAbilityRepository, AbilityRepository>();
            container.Bind<ISpellRepository, SpellRepository>();
            container.Bind<ISpellHandlerRepository, SpellHandlerRepository>();
        }
    }
}