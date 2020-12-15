using System;

namespace Rikarin {
    public class FilterException : Exception {
        public string[] Columns { get; }

        public FilterException(string[] columns) : base("filter") {
            Columns = columns;
        }
    }
}
