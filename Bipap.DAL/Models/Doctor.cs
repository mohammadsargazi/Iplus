using System;
using System.ComponentModel.DataAnnotations;

namespace Bipap.DAL.Models
{
    [DisplayTableName(Name = "دکتر")]
    public class Doctor : BaseModel
    {
        [Display(Name = "نام")]
        public string FirstName { get; set; }
        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; }
        [Display(Name = "موبایل")]
        public string Mobile { get; set; }
        [Display(Name = "کد نظام پزشکی")]
        public string MedicalSystemCode { get; set; }
        [Display(Name = "آدرس مطب")]
        public string OfficeAddress { get; set; }
        [Display(Name = "تاریخ ثبت نام")]
        public DateTime RegistrationDate { get; set; }
        [Display(Name = "نام کلینیک")]
        public string Clinic { get; set; }
        [NotListed]
        public string ActiveCode { get; set; }

        public override string ToString()
        {
            return FirstName + " " + LastName;
        }

    }
}
