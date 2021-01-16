using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bipap.DAL.Models
{
    [DisplayTableName(Name = "پشتیبان")]
    public class SupportUser : BaseModel
    {
        [Display(Name = "نام")]
        public string FirstName { get; set; }
        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; }
        [DataType(DataType.Upload)]
        [Display(Name = "آواتار")]
        public string ImageUrl { get; set; }
        [Display(Name = "آدرس")]
        public string Address { get; set; }
        [Display(Name = "شهر")]
        public string City { get; set; }
        [Display(Name = "موبایل")]
        public string Mobile { get; set; }
        [Display(Name = "شماره قرارداد")]
        public string ContractNumber { get; set; }
        [Display(Name = "شماره شبا")]
        public string ShabaNumber { get; set; }
        [Display(Name = "اعتبار")]
        public string Credit { get; set; }
        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; }
        [NotListed]
        public string ActiveCode { get; set; }
        public override string ToString()
        {
            return FirstName + " " + LastName;
        }
    }
}
