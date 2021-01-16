using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bipap.DAL.Models
{
    [DisplayTableName(Name = "سفارشات پشتیبان")]
    public class SupportUserOrder:BaseModel
    {
        [Display(Name = "تاریخ")]
        public DateTime Date { get; set; }
        [Display(Name = "تعداد")]
        public int Count { get; set; }
        [Display(Name = "مبلغ")]
        public decimal Price { get; set; }
        [Display(Name = "توضیحات")]
        public string Description { get; set; }
        #region NavigationProp
        [Display(Name = "پشتیبان")]
        public virtual SupportUser SupportUser { get; set; }
        [ForeignKey("SupportUser")]
        [Display(Name = "پشتیبان")]
        public int? SupportUserId { get; set; }
        [Display(Name = "وضعیت تسویه")]
        public virtual SettelmentStatus SettelmentStatus { get; set; }
        [ForeignKey("SettelmentStatus")]
        [Display(Name = "وضعیت تسویه")]
        public int? SettelmentStatusId { get; set; }
        #endregion
    }
}  
