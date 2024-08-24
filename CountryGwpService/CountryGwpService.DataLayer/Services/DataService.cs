using CountryGwpService.DataLayer.Models;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CountryGwpService.DataLayer.Services
{
    public class DataService : IDataService
    {
        public List<GwpData> CsvData { get; private set; } = new List<GwpData>();

        public void Initialize()
        {
            var csvData = new List<GwpData>();
            var basePath = Directory.GetCurrentDirectory(); // Base path for the application
            var filePath = Path.Combine(basePath, "Files", "gwpByCountry.csv");
            CsvData = LoadCsvData(filePath);
        }
        private List<GwpData> LoadCsvData(string filePath)
        {
            var data = new List<GwpData>();
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("CSV file not found", filePath);
            }

            using (var reader = new StreamReader(filePath))
            {
                var isFirstLine = true;
                var headers = reader.ReadLine()?.Split(',');
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    var values = line.Split(',');
                    if (isFirstLine)
                    {
                        isFirstLine = false;
                        continue;
                    }

                    var yearData = new Dictionary<int, double?>();
                    for (int i = 4; i < values.Length; i++)
                    {
                        if (double.TryParse(values[i], NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
                        {
                            yearData[i - 4 + 2000] = result;
                        }
                        else
                        {
                            yearData[i - 4 + 2000] = null;
                        }
                    }

                    data.Add(new GwpData
                    {
                        Country = values[0],
                        VariableId = values[1],
                        VariableName = values[2],
                        LineOfBusiness = values[3],
                        YearlyData = yearData
                    });
                }
            }

            return data;
        }

        //private List<GwpData> LoadCsvData()
        //{
        //    var csvData = new List<GwpData>();
        //    var basePath = Directory.GetCurrentDirectory(); // Base path for the application
        //    var filePath = Path.Combine(basePath, "Files", "gwpByCountry.csv");
        //    using (var reader = new StreamReader(filePath))
        //    using (var csv = new CsvReader(reader, new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)))
        //    {
        //        csvData = csv.GetRecords<GwpData>().ToList();
        //    }

        //    return csvData;
        //}
    }
}
