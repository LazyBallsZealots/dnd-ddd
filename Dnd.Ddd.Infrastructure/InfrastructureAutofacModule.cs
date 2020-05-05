using System.Collections.Generic;
using System.IO;
using System.Reflection;

using Autofac;

using Dnd.Ddd.Common.Infrastructure.Events;
using Dnd.Ddd.Common.Infrastructure.UnitOfWork;
using Dnd.Ddd.Common.ModelFramework;
using Dnd.Ddd.Infrastructure.Database.Middleware;
using Dnd.Ddd.Infrastructure.Database.Repository.Character;
using Dnd.Ddd.Infrastructure.Database.UnitOfWork;
using Dnd.Ddd.Model.Character.Repository;

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

            builder.Register(context => new CharacterRepository(context.Resolve<IUnitOfWork>())).As<ICharacterRepository>().InstancePerLifetimeScope();

            builder.Register(context => new NHibernateUnitOfWork(context.Resolve<ISession>()))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.Register(context => CreateEventStore(context.Resolve<ISessionFactory>()))
                .As<IDomainEventHandler<BaseDomainEvent>>()
                .SingleInstance();
        }

        protected virtual string HibernateConfigFilePath =>
            $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/hibernate.cfg.xml";

        protected IEnumerable<Assembly> MappingAssemblies => new List<Assembly>
        {
            Assembly.Load("Dnd.Ddd.Infrastructure.Database")
        };

        protected abstract ISessionFactory CreateSessionFactory(Configuration configuration);

        protected abstract Configuration BuildConfiguration(PostCommitEventListener eventListener);

        protected abstract IDomainEventHandler<BaseDomainEvent> CreateEventStore(ISessionFactory sessionFactory);
    }
}