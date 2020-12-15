using System.Collections.Generic;

namespace Rikarin.Domain {
    public abstract class AgregateRoot : AgregateRoot<long> {
        
    }

    public abstract class AgregateRoot<T> : Entity<T>, IAgregateRoot<T> {
        readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();
        public virtual IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents;


        public void AddDomainEvent(IDomainEvent domainEvent) {
            _domainEvents.Add(domainEvent);
        }

        public void ClearEvents() {
            _domainEvents.Clear();
        }
    }
}