using System.Threading;
using System.Threading.Tasks;
using System;

namespace Rikarin.Domain {
    public interface IStorageContext : IDisposable {
        void ExpireCache<T>();
        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
