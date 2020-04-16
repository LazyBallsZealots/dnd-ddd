using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dnd.Ddd.Common.ModelFramework;
using NHibernate.Event;
using NHibernate.Event.Default;

namespace Dnd.Ddd.Infrastructure.Database.Middleware
{
    public class SoftDeleteEventListener : DefaultDeleteEventListener
    {
        public override void OnDelete(DeleteEvent @event)
        {
            if (@event.Entity is Entity domainEntity)
            {
                base.CascadeBeforeDelete(
                    @event.Session,
                    @event.Session.GetEntityPersister(@event.EntityName, @event.Entity),
                    @event.Entity,
                    @event.Session.PersistenceContext.GetEntry(@event.Entity),
                    null);

                domainEntity.IsDeleted = true;
                
                base.CascadeAfterDelete(
                    @event.Session,
                    @event.Session.GetEntityPersister(@event.EntityName, @event.Entity),
                    @event.Entity,
                    null);
            }
            else
            {
                base.OnDelete(@event);
            }
        }

        public override void OnDelete(DeleteEvent @event, ISet<object> transientEntities)
        {
            if (@event.Entity is Entity domainEntity)
            {
                base.CascadeBeforeDelete(
                    @event.Session,
                    @event.Session.GetEntityPersister(@event.EntityName, @event.Entity),
                    @event.Entity,
                    @event.Session.PersistenceContext.GetEntry(@event.Entity),
                    transientEntities);

                domainEntity.IsDeleted = true;
                
                base.CascadeAfterDelete(
                    @event.Session,
                    @event.Session.GetEntityPersister(@event.EntityName, @event.Entity),
                    @event.Entity,
                    transientEntities);
            }
            else
            {
                base.OnDelete(@event, transientEntities);
            }
        }

        public override async Task OnDeleteAsync(DeleteEvent @event, CancellationToken cancellationToken)
        {
            if (@event.Entity is Entity domainEntity)
            {
                await base.CascadeBeforeDeleteAsync(
                    @event.Session,
                    @event.Session.GetEntityPersister(@event.EntityName, @event.Entity),
                    @event.Entity,
                    @event.Session.PersistenceContext.GetEntry(@event.Entity),
                    null,
                    cancellationToken);

                domainEntity.IsDeleted = true;

                await base.CascadeAfterDeleteAsync(
                    @event.Session,
                    @event.Session.GetEntityPersister(@event.EntityName, @event.Entity),
                    @event.Entity,
                    null,
                    cancellationToken);
            }

            else
            {
                await base.OnDeleteAsync(@event, cancellationToken);
            }
        }

        public override async Task OnDeleteAsync(DeleteEvent @event, ISet<object> transientEntities, CancellationToken cancellationToken)
        {
            if (@event.Entity is Entity domainEntity)
            {
                await base.CascadeBeforeDeleteAsync(
                    @event.Session,
                    @event.Session.GetEntityPersister(@event.EntityName, @event.Entity),
                    @event.Entity,
                    @event.Session.PersistenceContext.GetEntry(@event.Entity),
                    transientEntities,
                    cancellationToken);

                domainEntity.IsDeleted = true;

                await base.CascadeAfterDeleteAsync(
                    @event.Session,
                    @event.Session.GetEntityPersister(@event.EntityName, @event.Entity),
                    @event.Entity,
                    transientEntities,
                    cancellationToken);
            }

            else
            {
                await base.OnDeleteAsync(@event, transientEntities, cancellationToken);
            }
        }
    }
}