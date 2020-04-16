using Autofac;

using Dnd.Ddd.Common.Infrastructure.Events;
using Dnd.Ddd.Infrastructure.Database.Middleware;
using Dnd.Ddd.Infrastructure.Database.Repository.Character;
using Dnd.Ddd.Infrastructure.Database.Repository.Character.Saga;

using NHibernate;
using NHibernate.Cfg;

namespace Dnd.Ddd.Infrastructure.Database
{
    public abstract class InfrastructureAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(context => new PostCommitEventListener(context.Resolve<IDomainEventDispatcher>()))
                .As<PostCommitEventListener>()
                .SingleInstance();

            builder.Register(context => BuildConfiguration(context.Resolve<PostCommitEventListener>()))
                .As<Configuration>()
                .SingleInstance();

            builder.Register(context => CreateSessionFactory(context.Resolve<Configuration>())).As<ISessionFactory>().SingleInstance();

            builder.Register(context => new CharacterRepository(context.Resolve<ISession>()))
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.Register(context => new CharacterCreationSagaRepository(context.Resolve<ISessionFactory>().OpenSession()))
                .AsImplementedInterfaces()
                .SingleInstance();
        }

        protected abstract ISessionFactory CreateSessionFactory(Configuration configuration);

        protected abstract Configuration BuildConfiguration(PostCommitEventListener eventListener);
    }
}