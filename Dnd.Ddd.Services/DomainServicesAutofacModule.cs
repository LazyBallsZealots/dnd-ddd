using Autofac;

using Dnd.Ddd.Common.Infrastructure.UnitOfWork;
using Dnd.Ddd.Model.Character.Repository;
using Dnd.Ddd.Services.Commands.Handlers;
using Dnd.Ddd.Services.Queries.Handlers;

namespace Dnd.Ddd.Services
{
    public class DomainServicesAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(context => new ChooseCharacterNameCommandHandler(context.Resolve<ICharacterRepository>(), context.Resolve<IUnitOfWork>()))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.Register(context => new ChooseCharacterRaceCommandHandler(context.Resolve<ICharacterRepository>(), context.Resolve<IUnitOfWork>()))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.Register(context => new CreateChatacterDraftCommandHandler(context.Resolve<ICharacterRepository>(), context.Resolve<IUnitOfWork>()))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.Register(context => new RollAbilityScoresCommandHandler(context.Resolve<ICharacterRepository>(), context.Resolve<IUnitOfWork>()))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.Register(context => new GetCharacterByIdQueryHandler(context.Resolve<ICharacterRepository>()))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.Register(context => new GetCharactersByPlayerIdQueryHandler(context.Resolve<ICharacterRepository>()))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}