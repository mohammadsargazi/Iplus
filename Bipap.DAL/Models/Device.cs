using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Bipap.DAL.Models
{
    [DisplayTableName(Name = "دستگاه")]
    public class Device : BaseModel
    {
        [Display(Name = "شماره سریال")]
        public string SerialNumber { get; set; }
        [Display(Name = "تاریخ ثبت دستگاه (تولید)")]
        public DateTime RegistrationDate { get; set; }
        [Display(Name = "تاریخ واگذاری به بیمار")]
        public DateTime DeliveryDate { get; set; }

        #region NavigationProp
        [Display(Name = "وضعیت دستگاه")]
        public virtual DeviceStatus DeviceStatus { get; set; }
        [ForeignKey("DeviceStatus")]
        [Display(Name = "وضعیت دستگاه")]
        public int? DeviceStatusId { get; set; }
        [Display(Name = "نوع دستگاه")]
        public virtual DeviceType DeviceType { get; set; }
        [ForeignKey("DeviceType")]
        [Display(Name = "نوع دستگاه")]
        public int DeviceTypeId { get; set; }
        [Display(Name = "پشتیبان")]
        public virtual SupportUser SupportUser { get; set; }
        [ForeignKey("SupportUser")]
        [Display(Name = "پشتیبان")]
        public int? SupportUserId { get; set; }
        #endregion
        public override string ToString()
        {
            return SerialNumber;
        }
    }
}
