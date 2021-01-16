using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Bipap.DAL.Models
{
    [DisplayTableName(Name = "بیمار")]
    public class Patient : BaseModel
    {
        [Display(Name = "نام ونام خانوادگی")]
        public string FullName { get; set; }
        [Display(Name = "کد مشخصه بیمار")]
        public string PersonalId { get; set; }
        [Display(Name = "کد ملی")]
        public string NantionalCode { get; set; }
     
        [Display(Name = "آدرس")]
        public string Address { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "تاریخ تولد")]
        public DateTime DateOfBirth { get; set; }
        [Display(Name = "سن")]
        public int Age { get; set; }
        [Display(Name = "موبایل")]
        public string Mobile { get; set; }
        [Display(Name = "ایمیل")]
        public string Email { get; set; }
        [Display(Name = "نوع بیمه")]
        public string InsuranceKinde { get; set; }
        [Display(Name = "تاریخ اتمام بیمه")]
        public DateTime InsuranceDate { get; set; }
        [Display(Name = "کد بیمه")]
        public int InsuranceId { get; set; }

        [NotListed]
        public string ActiveCode { get; set; }

        #region NavigationProp
        [Display(Name = "جنسیت")]
        public virtual Gender Gender { get; set; }
        [Display(Name = "جنسیت")]
        [ForeignKey("Gender")]
        public int? GenderId { get; set; }
        [Display(Name = "دکتر")]
        public virtual Doctor Doctor { get; set; }
        [Display(Name = "دکتر")]
        [ForeignKey("Doctor")]
        public int? DoctorId { get; set; }
        [Display(Name = "دستگاه")]
        public  virtual Device Device { get; set; }
        [Display(Name = "دستگاه")]
        [ForeignKey("Device")]
        public int? DeviceId { get; set; }
        #endregion
        public override string ToString()
        {
            return FullName;
        }
    }
}
