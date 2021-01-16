using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bipap.Service.IServices;
using Microsoft.AspNetCore.Mvc;
using SupportUserPanel.Functionality;
using SupportUserPanel.Models;

namespace SupportUserPanel.Controllers
{
    public class EndOfTreatmentController : Controller
    {
        #region Fields
        private readonly IEndOfTreatmentService _endOfTreatmentService;
        private readonly IDeviceService _deviceService;
        #endregion

        #region Const
        public EndOfTreatmentController(IEndOfTreatmentService endOfTreatmentService, IDeviceService deviceService)
        {
            _endOfTreatmentService = endOfTreatmentService;
            _deviceService = deviceService;
        }
        #endregion

        #region Actions
        public IActionResult Add(int id)
        {
            var userId = HttpContextMethod.GetCurrentUserId(User);
            var model = _deviceService.GetWithModeAndPatient(id);
            return View(model);
        }
        [HttpPost]
        public IActionResult Add(EndOfTreatmentViewModel model)
        {
            model.SupportUserId = HttpContextMethod.GetCurrentUserId(User);
            var endOfTreatmentModel = model.ToModel();
            if (endOfTreatmentModel == null)
                return Json(new { res = false, message = "عملیات با موفقیت انجام نشد." });
            _endOfTreatmentService.Create(endOfTreatmentModel);
            return Json(new { res = true, message = "عملیات با موفقیت انجام شد." });
        }
        #endregion
    }
}
