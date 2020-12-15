namespace Rikarin.Filtering {
    public class Sort {
        public string Name { get; set; }
        public SortDirection Direction { get; set; }

        public Sort() { }

        public Sort(string name, SortDirection direction) {
            Name = name;
            Direction = direction;
        }
    }
}
