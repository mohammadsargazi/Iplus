using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileService.Model
{
    public class SupportUserViewModel
    {
        public int Id { get; set; }
        public string Mobile { get; set; }
        public string Token { get; set; }
        public string ActiveCode { get; set; }
    }
    public class PatientViewMode
    {
        public int Id { get; set; }
        public string Mobile { get; set; }
        public string NationalCode { get; set; }
        public string Token { get; set; }
        public string ActiveCode { get; set; }
    }
}
