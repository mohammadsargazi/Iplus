using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPlusApp.Functionality
{
    public class WkHtmlToPdfException : Exception
    {
        /// <summary>Get WkHtmlToPdf process error code</summary>
        public int ErrorCode { get; private set; }

        public WkHtmlToPdfException(int errCode, string message)
          : base(string.Format("{0} (exit code: {1})", (object)message, (object)errCode))
        {
            this.ErrorCode = errCode;
        }
    }
}
