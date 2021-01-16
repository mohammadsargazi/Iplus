using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bipap.Service.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobileService.Functionality;
using MobileService.Model;

namespace MobileService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        #region Fileds
        private readonly IPatientService _patientService;
        private readonly ISupportUserService _supportUserService;
        //private readonly ISupportUserDeviceService _supportUserDeviceService;
        private readonly IPrescriptionService _prescriptionService;
        private readonly SecurityHandler securityHandler = new SecurityHandler();
        #endregion

        #region Const
        public DashboardController(IPatientService patientService
            , ISupportUserService supportUserService
            //, ISupportUserDeviceService supportUserDeviceService
            , IPrescriptionService prescriptionService)
        {
            _patientService = patientService;
            _supportUserService = supportUserService;
            //_supportUserDeviceService = supportUserDeviceService;
            _prescriptionService = prescriptionService;
        }
        #endregion

        #region PatientActions
        [HttpPost("PatientInformation")]
        public ResponseModel PatientInformation()
        {
            var patientId = securityHandler.GetUserId(User);
            var patient = _patientService.GetPatientWithDoctorAndDevice(patientId);
            var lastPrescription = _prescriptionService.GetLastPrescriptionByPatientId(patientId);
            var res = patient.ToModel(lastPrescription);
            return res.ToResponseModel();

        }
        #endregion

        #region SupportUserActions
        #endregion

    }
}
