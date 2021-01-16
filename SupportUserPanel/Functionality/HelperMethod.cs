using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupportUserPanel.Functionality
{
    public static class HelperMethod
    {
        public static string GenerateRandomCode()
        {
            Random rnd = new Random();
            return rnd.Next(1000, 9999).ToString();
        }
    }
}
