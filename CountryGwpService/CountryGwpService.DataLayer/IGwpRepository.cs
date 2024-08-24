using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountryGwpService.DataLayer
{
    public interface IGwpRepository
    {
        Task<Dictionary<string, double>> GetAverageGwpAsync(string country, List<string> lob);
    }
}
