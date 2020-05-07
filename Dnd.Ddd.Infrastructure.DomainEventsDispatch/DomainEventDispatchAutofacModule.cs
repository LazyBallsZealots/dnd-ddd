using System;
using System.Collections.Generic;
using System.Linq;

using Autofac;
using Autofac.Features.Variance;

using Dnd.Ddd.Common.Infrastructure.Events;

using MediatR;

using NHibernate;

namespace Dnd.Ddd.Infrastructure.EventBus
{
    public class DomainEventDispatchAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterSource(new ContravariantRegistrationSource());

            builder.RegisterType<Mediator>().As<IMediator>().SingleInstance();

            builder.Register<ServiceFactory>(
                context =>
                {
                    var c = context.Resolve<IComponentContext>();
                    return t => c.Resolve(t);
                });

            foreach (var notificationType in GetTypesImplementingInterface(typeof(INotification)))
            {
                builder.RegisterType(notificationType).AsImplementedInterfaces().InstancePerLifetimeScope();
            }

            foreach (var domainEventHandlerType in GetTypesImplementingInterface(typeof(IDomainEventHandler<>)))
            {
                builder.RegisterType(domainEventHandlerType).AsImplementedInterfaces().InstancePerLifetimeScope();
            }

            builder.Register(context => new EventStore.EventStore(context.Resolve<ISessionFactory>()))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<EventDispatcher.EventDispatcher>().As<IDomainEventDispatcher>().SingleInstance();
        }

        private static IEnumerable<Type> GetTypesImplementingInterface(Type @interface) =>
            AppDomain.CurrentDomain.GetAssemblies()
                .Where(x => !x.IsDynamic)
                .SelectMany(
                    assembly => assembly.GetExportedTypes()
                        .Where(
                            type => type.GetInterfaces()
                                        .Any(i => i == @interface || i.IsGenericType && i.GetGenericTypeDefinition() == @interface) &&
                                    !type.IsAbstract &&
                                    type.IsInNamespace("Dnd.Ddd")))
                .ToList();
    }
}