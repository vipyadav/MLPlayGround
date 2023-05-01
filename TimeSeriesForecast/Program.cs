using ExportToOnnx;
using Microsoft.ML;
using Microsoft.ML.Transforms.TimeSeries;
using TimeSeriesForecast;


//CSVHelper.ReadCSVAndWriteFileLineByLine();

OnnxProgram onnxProgram = new OnnxProgram();
onnxProgram.Execute();

string MODEL_NAME = "model.onnx";

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
var custommodel = model.Transform(data);

//Convert trained model to ONNX Model
using (var stream = File.Create(MODEL_NAME))
{
    context.Model.ConvertToOnnx(model, data, stream);
}


var forecastingEngine = model.CreateTimeSeriesEngine<MeasurementData, MeasurementForecast>(context);

var forecasts = forecastingEngine.Predict();

var outPutFile = "./../../prediction.csv";
File.WriteAllText(outPutFile, "ForeCast Values: \n");

foreach (var forecast in forecasts.Forecast)
{
    try
    {
        File.AppendAllText(outPutFile, $"{forecast.ToString()}\n");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Data could not be written to the CSV file.");
        // return;
    }
    Console.WriteLine(forecast);
}

Console.ReadLine();

