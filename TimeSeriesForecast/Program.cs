using Microsoft.ML;
using Microsoft.ML.Transforms.TimeSeries;
using TimeSeriesForecast;

var context = new MLContext();

var data = context.Data.LoadFromTextFile<MeasurementData>(@"./MeasurementHistoryData.csv",
            hasHeader: true, separatorChar: ',');

var pipeline = context.Forecasting.ForecastBySsa(
                nameof(MeasurementForecast.Forecast),
                nameof(MeasurementData.Value),
                windowSize: 5,
                seriesLength: 10,
                trainSize: 100,
                horizon: 5);

var model = pipeline.Fit(data);

var forecastingEngine = model.CreateTimeSeriesEngine<MeasurementData, MeasurementForecast>(context);

var forecasts = forecastingEngine.Predict();

foreach (var forecast in forecasts.Forecast)
{
    Console.WriteLine(forecast);
}

Console.ReadLine();