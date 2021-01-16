using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Bipap.DAL.Models
{
    [DisplayTableName(Name = "درخواست پایان درمان")]
    public class EndOfTreatment:BaseModel
    {
        [Display(Name = "تاریخ درخواست")]
        public DateTime Date { get; set; }
        [Display(Name = "تاریخ خرید بیمار")]
        public DateTime PatientPurchaseDate { get; set; }
        #region NavigationProp
        [Display(Name = "وضعیت درخواست")]
        public virtual EndOfTreatmentStatus EndOfTreatmentStatus { get; set; }
        [Display(Name = "وضعیت درخواست")]
        [ForeignKey("EndOfTreatmentStatus")]
        public int EndOfTreatmentStatusId { get; set; }
        [Display(Name = "پشتیبان")]
        public virtual SupportUser SupportUser { get; set; }
        [Display(Name = "پشتیبان")]
        [ForeignKey("SupportUser")]
        public int SupportUserId { get; set; }
        [Display(Name = "دستگاه")]
        public virtual Device Device { get; set; }
        [Display(Name = "دستگاه")]
        [ForeignKey("Device")]
        public int DeviceId { get; set; }
        #endregion
    }
}
