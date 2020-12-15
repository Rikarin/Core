namespace Rikarin.Filtering {
    public sealed class Pagination {
        public int Index { get; set; }
        public int Limit { get; set; }

        public Pagination() { }

        public Pagination(int index, int limit) {
            Index = index;
            Limit = limit;
        }
    }
}
