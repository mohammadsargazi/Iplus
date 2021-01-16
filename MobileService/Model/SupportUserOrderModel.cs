using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileService.Model
{
    public class SupportUserOrderModel
    {
        public int Count { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int SettelmentStatusId { get; set; }
    }
}
