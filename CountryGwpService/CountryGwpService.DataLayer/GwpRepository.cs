using CountryGwpService.DataLayer.Models;
using CountryGwpService.DataLayer.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CountryGwpService.DataLayer
{
    public class GwpRepository : IGwpRepository
    {
        private readonly IDataService _dataService;

        public GwpRepository(IDataService dataService)
        {
            _dataService = dataService;
        }


        public async Task<Dictionary<string, double>> GetAverageGwpAsync(string country, List<string> lob)
        {

            await Task.Delay(100);

            var filteredData = _dataService.CsvData
             .Where(d => d.Country == country && lob.Contains(d.LineOfBusiness))
             .ToList();

            var lobAverages = new Dictionary<string, double>();

            foreach (var lobType in lob)
            {
                var gwps = filteredData
                    .Where(d => d.LineOfBusiness == lobType)
                    .SelectMany(d => d.YearlyData.Where(x => x.Key >= 2008 && x.Key <=2015).Select(x => x.Value))
                    .Where(x => x.HasValue)
                    .ToList();

                if (gwps.Any())
                {
                    var average = gwps.Average(x => x.Value);
                    lobAverages[lobType] = average;
                }
                else
                {
                    lobAverages[lobType] = 0;
                }
            }

            return lobAverages;
        }
    }
}
