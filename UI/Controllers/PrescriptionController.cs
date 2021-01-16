using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bipap.DAL.Extentions;
using Bipap.DAL.Models;
using Bipap.DAL.ViewModels;
using Bipap.Service.IServices;
using Microsoft.AspNetCore.Mvc;
using UI.Functionality;

namespace UI.Controllers
{
    public class PrescriptionController : Controller
    {
        #region Fields
        private readonly IPrescriptionService _prescriptionService;
        private readonly IPatientService _patientService;
        private readonly IDeviceTypeService _deviceTypeServic;
        private readonly IDeviceTypeInformationService _deviceTypeInformationService;

        #endregion

        #region Const
        public PrescriptionController(IPrescriptionService prescriptionService, IPatientService patientService,
            IDeviceTypeService deviceTypeService,IDeviceTypeInformationService deviceTypeInformationService)
        {
            _prescriptionService = prescriptionService;
            _patientService = patientService;
            _deviceTypeServic = deviceTypeService;
            _deviceTypeInformationService = deviceTypeInformationService;

        }
        #endregion
        public async Task<IActionResult> Index(int patientId)
        {
            ViewBag.Patient = await _patientService.GetAsync(patientId);
            return View();
        }
        public async Task<IActionResult> List(int patientId)
        {
            var model = await _prescriptionService.GetByPatientIdAsync(patientId);
            return View(model);
        }
        public async Task<IActionResult> Delete(int prescriptionId)
        {
            var presciption = await _prescriptionService.GetAsync(prescriptionId);
            if (presciption != null)
            {
                var res = await _prescriptionService.DeleteAsync(presciption);
                return Json(new { res = true, message = "عملیات حذف با موفقیت انجام شد." });
            }
            return Json(new { res = true, message = "عملیات حذف با موفقیت انجام نشد." });

        }
        public async Task<IActionResult> ShowMore(int presciptionId)
        {
            var presciption = await _prescriptionService.GetPrescriptionWithPatientById(presciptionId);
            return View(presciption);
        }
        public async Task<IActionResult> Add(int patientId)
        {
            var deviceType = await _deviceTypeServic.GetAllAsync();
            ViewBag.DeviceType = deviceType.ToDictionary(x => x.Id, x => x.Title);
            ViewBag.PatientId = patientId;
            return View(new Prescription());
        }
        public async Task<IActionResult> Edit(int prescriptionId)
        {
            var model = await _prescriptionService.GetAsync(prescriptionId);
            var deviceType = await _deviceTypeServic.GetAllAsync();
            ViewBag.DeviceType = deviceType.ToDictionary(x => x.Id, x => x.Title);
            ViewBag.PatientId = model.PatientId;
            return View("Add", model);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrEdit(PrescriptionViewModel model)
        {
            var doctorId= HttpContextMethod.GetCurrentUserId(User);
            var newModel = model.ToModel(model.PatientId, doctorId);
            if (newModel != null)
            {
                if (model.Id == 0)
                    await _prescriptionService.CreateAsync(newModel);
                else
                    await _prescriptionService.UpdateAsync(newModel);
                return Json(new { res = true, message = "اطلاعات با موفقیت ذخیره شد." });
            }
            return Json(new { res = false, message = "اطلاعات با موفقیت ذخیره نشد." });
        }
        [HttpPost]
        public IActionResult GetDeviceTypeInformation(int deviceTypeId)
        {
            var model = _deviceTypeInformationService.GetByDeviceTypeId(deviceTypeId)
                .Select(x => x.ConvertNull()).ToList();
            return Json(new { data = model });
        }
    }
}
