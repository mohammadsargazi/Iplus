using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Bipap.DAL.Models
{
    [DisplayTableName(Name = "نسخه")]
    public class Prescription:BaseModel
    {
        [Display(Name = "Range")]
        public string Range { get; set; }

        [Display(Name = "تاریخ صدور")]
        public DateTime IssueDate { get; set; }
        [Display(Name = "تاریخ انجام")]
        public DateTime CompletionDate { get; set; }
        [Display(Name = "مدت زمان درمان")]
        public string DurationOfTreatment { get; set; }

        #region NavigationProp
        [Display(Name = "بیمار")]
        public virtual Patient Patient { get; set; }
        [Display(Name = "بیمار")]
        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        [Display(Name = "دکتر")]
        public virtual Doctor Doctor { get; set; }
        [Display(Name = "دکتر")]
        [ForeignKey("Doctor")]
        public int? DoctorId { get; set; }
        [Display(Name = "نوع دستگاه")]
        public virtual DeviceType DeviceType { get; set; }
        [Display(Name = "نوع دستگاه")]
        [ForeignKey("DeviceType")]
        public int? DeviceTypeId { get; set; }
        [Display(Name = "وضعیت نسخه")]
        public virtual PrescriptionStatus PrescriptionStatus { get; set; }
        [Display(Name = "وضعیت نسخه")]
        [ForeignKey("PrescriptionStatus")]
        public int? PrescriptionStatusId { get; set; }

        #endregion
        public override string ToString()
        {
            return Patient.FullName;
        }
    }
}
