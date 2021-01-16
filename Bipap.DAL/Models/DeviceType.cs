using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bipap.DAL.Models
{
    [DisplayTableName(Name = "نوع دستگاه")]
    public class DeviceType:BaseModel
    {
        [Display(Name = "عنوان")]
        public string Title { get; set; }
        [Display(Name = "قیمت")]
        public decimal Price { get; set; }
        [Display(Name = "سطح")]
        public int Level { get; set; }
        public override string ToString()
        {
            return Title;
        }
    }
}
