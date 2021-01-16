using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bipap.Service.IServices;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    public class PrescriptionAndFileController : Controller
    {
        #region Fields
        private readonly IPrescriptionService _prescriptionService;
        private readonly IPatientService _patientService;
        private readonly IFileService _fileService;
        //private readonly IModeService _modeService;
        #endregion

        #region Const
        public PrescriptionAndFileController(IPrescriptionService prescriptionService, IPatientService patientService
            /*, IModeService modeService*/,IFileService fileService)
        {
            _prescriptionService = prescriptionService;
            _patientService = patientService;
            //_modeService = modeService;
            _fileService = fileService;
        }
        #endregion
        public IActionResult Index(int patientId)
        {
            ViewBag.Patient = _patientService.Get(patientId);
            ViewBag.Prescriptions = _prescriptionService.GetByPatientId(patientId);
            ViewBag.Files = _fileService.GetByPatientId(patientId);
            return View();
        }
    }
}
