using Microsoft.ML.Data;

namespace TimeSeriesForecast
{
    public class MeasurementData
    {
        //public MeasurementData(DateTime timestamp, double value)
        //{
        //    Timestamp = timestamp;
        //    Value = value;
        //}

        [LoadColumn(0)]
        public DateTime Timestamp { get; set; }

        [LoadColumn(1)]
        public float Value { get; set; }
    }
}
