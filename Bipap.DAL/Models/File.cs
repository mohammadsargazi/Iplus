using System;
using System.Collections.Generic;
using System.Text;

namespace Bipap.DAL.Models
{
    public class File:BaseModel
    {
        public int SessionCount { get; set; }
        public int DayCount { get; set; }
        public int HoursActivity { get; set; }

        #region NavigationProp
        public virtual Patient Patient { get; set; }
        public int PatientId { get; set; }
        public virtual FileUploadType FileUploadType { get; set; }
        public int? FileUploadTypeId { get; set; }
        #endregion

    }
}
