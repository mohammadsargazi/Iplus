using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bipap.DAL.Extentions;
using Bipap.DAL.ViewModels;
using Bipap.Service.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UI.Functionality;
using UI.Models;

namespace UI.Controllers
{
    [Authorize]
    public class PatientController : Controller
    {
        #region Fields
        private readonly IPatientService _patientService;
        private readonly IDoctorService _doctorService;
        #endregion

        #region Const
        public PatientController(IPatientService patientService, IDoctorService doctorService)
        {
            _patientService = patientService;
            _doctorService = doctorService;
        }
        #endregion

        #region Actions
        public IActionResult List()
        {
            var currentUserId = HttpContextMethod.GetCurrentUserId(User);
            var res = _patientService.GetPatientsByDoctorId(currentUserId);
            return View(res);
        }
        public IActionResult Add(PatientViewModel patientViewModel)
        {
            var patient = _patientService.GetByNationalCode(patientViewModel.PersonalId);
            if (patient != null)
                return Json(new { res = false, message = "بیماری با این کد ملی قبلا درج شده است." });
            var currentUserId = HttpContextMethod.GetCurrentUserId(User);
            var model = patientViewModel.ToModel(currentUserId);
            var res = _patientService.Create(model);
            return Json(new { res = true });
        }
        public IActionResult Delete(int id)
        {
            var patient = _patientService.Get(id);
            if (patient != null)
            {
                var res = _patientService.Delete(patient);
                return Json(new { res = true, message = "عملیات حذف بیمار با موفقیت انجام شد." });
            }
            return Json(new { res = false, message = "بیماری با این مشخصات پیدا نشد." });
        }
        #endregion
    }
}
