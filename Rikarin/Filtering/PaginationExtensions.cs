using System.Linq;
using System.Collections.Generic;

namespace Rikarin.Filtering {
    public static class PaginationExtensions {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> query, Pagination pagination) {
            if (pagination != null) {
                query = query.Skip(pagination.Index).Take(pagination.Limit);
            }

            return query;
        }

        public static IEnumerable<T> Paginate<T>(this IEnumerable<T> query, Pagination pagination) {
            if (pagination != null) {
                query = query.Skip(pagination.Index).Take(pagination.Limit);
            }

            return query;
        }
    }
}
