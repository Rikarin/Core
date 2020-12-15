using System.Collections.Generic;

namespace Rikarin.Filtering {
    public class Selection {
        public IEnumerable<Filter> Filters { get; set; }
        public IEnumerable<Sort> Sorts { get; set; }
        public Pagination Pagination { get; set; }

        public Selection() { }

        public Selection(IEnumerable<Filter> filters, IEnumerable<Sort> sorts, Pagination pagination) {
            Filters = filters;
            Sorts = sorts;
            Pagination = pagination;
        }
    }
}
