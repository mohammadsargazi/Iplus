using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileService.Model
{
    public class PatientModel
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? Age { get; set; }
        public string Email { get; set; }
        public int? GenderId { get; set; }
    }
}
