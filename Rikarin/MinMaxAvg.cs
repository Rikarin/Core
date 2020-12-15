namespace Rikarin {
    public class MinMaxAvg {
        public double Min { get; private set; }
        public double Max { get; private set; }
        public double Avg { get; private set; }

        public MinMaxAvg(double min, double max, double avg) {
            Min = min;
            Max = max;
            Avg = avg;
        }
    }
}
