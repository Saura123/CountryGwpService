using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountryGwpService.DataLayer.Models
{
    public class GwpData
    {
        public string Country { get; set; }
        public string VariableId { get; set; }
        public string VariableName { get; set; }
        public string LineOfBusiness { get; set; }
        public Dictionary<int, double?> YearlyData { get; set; }
    }
}
