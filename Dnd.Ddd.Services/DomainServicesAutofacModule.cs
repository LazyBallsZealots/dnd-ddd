using Autofac;

using Dnd.Ddd.Model.Character.Repository;
using Dnd.Ddd.Services.Commands.Handlers;

namespace Dnd.Ddd.Services
{
    public class DomainServicesAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(context => new ChooseCharacterNameCommandHandler(context.Resolve<ICharacterRepository>()))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.Register(context => new ChooseCharacterRaceCommandHandler(context.Resolve<ICharacterRepository>()))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.Register(context => new CreateChatacterDraftCommandHandler(context.Resolve<ICharacterRepository>()))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.Register(context => new RollAbilityScoresCommandHandler(context.Resolve<ICharacterRepository>()))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}