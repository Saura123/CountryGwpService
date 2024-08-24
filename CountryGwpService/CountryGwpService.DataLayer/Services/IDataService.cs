using CountryGwpService.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CountryGwpService.DataLayer.Services
{
    public interface IDataService
    {
        List<GwpData> CsvData { get; }

        void Initialize();
    }

}
