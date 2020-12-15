namespace Rikarin.Filtering {
    public enum FilterCondition {
        IsEmpty,
        IsNotEmpty,
        IsEqual,
        IsNotEqual,
        BeginsWith,
        EndsWith,
        Contains,
        DoesNotContains,

        // Numeric
        IsGreather,
        IsGreatherOrEqual,
        IsLess,
        IsLessOrEqual
    }
}
