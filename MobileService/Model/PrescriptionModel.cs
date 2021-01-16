using Bipap.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileService.Model
{
   
    public class PrescriptionModel
    {
        public string SupportUserFullName { get; set; }
        public string SupportUserMobile { get; set; }
        public string DoctorFullName { get; set; }
        public string DeviceType { get; set; }
        public string PrescriptionStatus { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime CompletionDate { get; set; }
        public string DurationOfTreatment { get; set; }
        public List<PrescriptionInformationModel> PrescriptionInformationModels { get; set; }
    }
    public class PrescriptionInformationModel
    {
        public string Title { get; set; }
        public string Range { get; set; }
        public string RangeFrom { get; set; }
        public string RangeTo { get; set; }
        public string Resolution { get; set; }
        public string Unit { get; set; }
    }
}
