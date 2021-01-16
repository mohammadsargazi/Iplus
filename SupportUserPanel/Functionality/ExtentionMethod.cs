using Bipap.DAL.Models;
using SupportUserPanel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupportUserPanel.Functionality
{
    public static class ExtentionMethod
    {
        public static EndOfTreatment ToModel(this EndOfTreatmentViewModel model)
        {
            if (model == null)
                return null;
            return new EndOfTreatment
            {
                Date = DateTime.Now,
                EndOfTreatmentStatusId = 1,
                SupportUserId = model.SupportUserId,
                DeviceId = model.DeviceId
            };
        }
    }
}
