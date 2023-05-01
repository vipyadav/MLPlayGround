using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeSeriesForecast
{
    public static class CSVHelper
    {
        public static void ReadCSVAndWriteFileLineByLine()
        {
            StreamReader csvreader = new StreamReader(@"C:\Users\reeti\Downloads\bpx-december-data.csv\bpx-december-data.csv");
            string inputLine = "";
            while ((inputLine = csvreader.ReadLine()) != null)
            {

                try
                {
                    inputLine = inputLine.Trim().Replace("\"", "");

                    string[] csvArray = inputLine.Split(',', StringSplitOptions.TrimEntries);

                    var newLine = $"{Convert.ToDateTime(csvArray[3])},{float.Parse(csvArray[2])}\n";

                    File.AppendAllText(@"C:\D\VipGitHub\MLPlayGround\TimeSeriesForecast\MeasurementHistoryData.csv", newLine);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Data could not be written to the CSV file.");
                    // return;
                }
            }
        }
    }
}
