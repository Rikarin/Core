using System.Threading.Tasks;

namespace Rikarin.Domain {
    public interface IHandler<T> where T : IDomainEvent {
        Task Handle(T domainEvent);
    }
}