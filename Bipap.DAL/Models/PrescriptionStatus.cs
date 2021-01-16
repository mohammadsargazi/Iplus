using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bipap.DAL.Models
{
    [DisplayTableName(Name = "وضعیت نسخه")]
    public class PrescriptionStatus : BaseModel
    {
        [Display(Name = "عنوان")]
        public string Title { get; set; }
        public override string ToString()
        {
            return Title;
        }
    }
}
