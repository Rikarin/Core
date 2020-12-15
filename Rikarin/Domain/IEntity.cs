namespace Rikarin.Domain {
    public interface IEntity<T> {
        T Id { get; }
        bool IsTransient { get; }
    }
}
