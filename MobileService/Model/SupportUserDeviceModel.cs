using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileService.Model
{
    public class SupportUserDeviceModel
    {
        public int DeviceId { get; set; }
        public string DeviceStatus { get; set; }
        public string DeviceType { get; set; }
        public string DeviceSerialNumber { get; set; }
        public string PatientFullName { get; set; }
        public string DoctorFullName { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string PatientMobile { get; set; }
        public int? EndOfTreatmentId { get; set; }
        public bool HasEndOfTreatment { get; set; }
        public string EndOfTreatmentStatus { get; set; }
        public List<SupportUserDevicePrescription> Prescriptions { get; set; }
    }
    public class SupportUserDevicePrescription
    {
        public string Title { get; set; }
        public string Range { get; set; }
        public string RangeFrom { get; set; }
        public string RangeTo { get; set; }
        public string Resolution { get; set; }
        public string Unit { get; set; }
    }
}
