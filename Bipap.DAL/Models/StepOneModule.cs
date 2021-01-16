using System;
using System.Collections.Generic;
using System.Text;

namespace Bipap.DAL.Models
{
    public class StepOneModule : BaseModel
    {

        public string Name { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public string Day { get; set; }
        public string Hour { get; set; }
        public string Minutes { get; set; }
        public string StrarTime { get; set; }
        public string EndTime { get; set; }
        public string SessionLength { get; set; }
        public string Parameters { get; set; }
        public string Pressure { get; set; }
        public string Flow { get; set; }
        public string Heater { get; set; }
        public string Leak { get; set; }
        public long Length { get; set; }
        #region NavigationProp
        public virtual File File { get; set; }
        public int FileId { get; set; }
        #endregion
    }
}
