using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileService.Model
{
    public class PatientInformationModel
    {
        public string PatientFullName { get; set; }
        public string PatientNationalCode { get; set; }
        public string DeviceType { get; set; }
        public string DeviceNumber { get; set; }
        public string DoctorFullName { get; set; }
        public string SupportUserFullName { get; set; }
        public string SupportUserMobile { get; set; }
        public DateTime? LastPrescriptionDate { get; set; }
        public string LastPrescriptionStatus { get; set; }



    }
}
