using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.Models
{
    public class HomeIndexViewModel
    {
        public int SupportUserOrderCount { get; set; }
        public int PatientCount { get; set; }
        public int EndOfTreatmentCount { get; set; }
        public int DeviceCount { get; set; }
    }
}
