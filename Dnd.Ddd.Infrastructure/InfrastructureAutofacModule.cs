using Autofac;

using Dnd.Ddd.Common.Infrastructure.Events;
using Dnd.Ddd.Infrastructure.Middleware;
using Dnd.Ddd.Infrastructure.UnitOfWork;

using NHibernate;
using NHibernate.Cfg;

namespace Dnd.Ddd.Infrastructure
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

            builder.RegisterType<NHibernateUnitOfWork>().AsImplementedInterfaces().InstancePerLifetimeScope();
        }

        protected abstract ISessionFactory CreateSessionFactory(Configuration configuration);

        protected abstract Configuration BuildConfiguration(PostCommitEventListener eventListener);
    }
}