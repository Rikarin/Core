namespace Rikarin.Domain {
    public interface IAgregateRoot<T> : IEntity<T> {
        void AddDomainEvent(IDomainEvent domainEvent);
        void ClearEvents();
    }
}
