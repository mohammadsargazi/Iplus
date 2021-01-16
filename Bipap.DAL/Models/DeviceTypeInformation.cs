using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Bipap.DAL.Models
{
    [DisplayTableName(Name = "اطلاعات پایه ی نوع دستگاه")]
    public class DeviceTypeInformation:BaseModel
    {
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Display(Name = "RangeFrom")]
        public string RangeFrom { get; set; }
        [Display(Name = "RangeTo")]
        public string RangeTo { get; set; }
        [Display(Name = "Resolution")]
        public string Resolution { get; set; }
        [Display(Name = "Unit")]
        public string Unit { get; set; }

        #region NavigationProp
        [Display(Name = "نوع دستگاه")]
        public virtual DeviceType DeviceType { get; set; }
        [Display(Name = "نوع دستگاه")]
        [ForeignKey("DeviceType")]
        public int DeviceTypeId { get; set; }
        #endregion
    }
}
