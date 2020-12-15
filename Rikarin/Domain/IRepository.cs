using System.Threading.Tasks;
using System.Collections.Generic;

namespace Rikarin.Domain {
    public interface IRepository<T> : IRepository<T, long> {
        
    }

    public interface IRepository<T, U> {
        IStorageContext StorageContext { get; }

        Task<T> FindAsync(U id);
        T Add(T value);
        void Update(T value);
        void Remove(T value);
        void Remove(U id);
        void Remove(IEnumerable<U> ids);
    }
}
