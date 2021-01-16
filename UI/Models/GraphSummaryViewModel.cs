using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Models
{
    public class GraphSummaryViewModel
    {
        public string Pressure { get; set; }
        public string Flow { get; set; }
        public string TidalVolume { get; set; }
        public string Leak { get; set; }
    }
}
