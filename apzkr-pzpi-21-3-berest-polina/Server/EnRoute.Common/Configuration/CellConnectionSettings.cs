using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnRoute.Common.Configuration
{
    public class CellConnectionSettings
    {
        public string BaseUrl { get; set; } = string.Empty;
        public decimal NormalTemperature { get; set; } = 4.0M;
        public int OpenCount { get; set; } = 0;
    }
}
