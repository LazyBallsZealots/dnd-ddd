using System.IO;
using System.Reflection;

using Autofac;

using Dnd.Ddd.Common.Infrastructure.Events;
using Dnd.Ddd.Common.ModelFramework;
using Dnd.Ddd.Infrastructure.Database.Middleware;
using Dnd.Ddd.Infrastructure.Database.Repository.Character;

using NHibernate;
using NHibernate.Cfg;

using Module = Autofac.Module;

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

            builder.Register(context => context.Resolve<ISessionFactory>().OpenSession()).As<ISession>().InstancePerLifetimeScope();

            builder.Register(context => new CharacterRepository(context.Resolve<ISession>())).As<ISession>().InstancePerLifetimeScope();

            builder.Register(context => CreateEventStore(context.Resolve<ISessionFactory>()))
                .As<IDomainEventHandler<BaseDomainEvent>>()
                .SingleInstance();
        }

        protected virtual string HibernateConfigFilePath =>
            $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/hibernate.cfg.xml";

        protected abstract ISessionFactory CreateSessionFactory(Configuration configuration);

        protected abstract Configuration BuildConfiguration(PostCommitEventListener eventListener);

        protected abstract IDomainEventHandler<BaseDomainEvent> CreateEventStore(ISessionFactory sessionFactory);
    }
}