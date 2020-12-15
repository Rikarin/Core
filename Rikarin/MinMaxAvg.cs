namespace Rikarin {
    public record MinMax(double Min, double Max);
    public record MinMaxAvg(double Min, double Max, double Avg) : MinMax(Min, Max);
}
