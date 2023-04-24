using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
