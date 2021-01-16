using System;
using System.Collections.Generic;
using System.Text;

namespace Bipap.DAL.ViewModels
{
    public class PrescriptionViewModel
    {
        public int Id { get; set; }
        public string Range { get; set; }
        public int PatientId { get; set; }
        public int DeviceTypeId { get; set; }
    }
}
