namespace Rikarin.Filtering {
    public sealed class Filter {
        public FilterCondition Condition { get; set; }
        public string Column { get; set; }
        public string Value { get; set; }

        public Filter() { }

        public Filter(FilterCondition condition, string column, string value) {
            Condition = condition;
            Column = column;
            Value = value;
        }
    }
}
