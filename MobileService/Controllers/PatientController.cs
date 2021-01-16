using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bipap.DAL;
using Bipap.DAL.Models;
using Bipap.Service.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobileService.Functionality;
using MobileService.Model;

namespace MobileService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        #region Fileds
        private readonly IPatientService _patientService;
        private readonly SecurityHandler securityHandler = new SecurityHandler();
        #endregion

        #region Const
        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }
        #endregion

        #region Actions
        [HttpGet("Get")]
        public ResponseModel Get()
        {
            var patientId = securityHandler.GetUserId(User);
            var res= _patientService.Get(patientId);
            return res.ToResponseModel();
        }
        [HttpPost("Update")]
        public ResponseModel Update(PatientModel model)
        {
            var patientId = securityHandler.GetUserId(User);
            var patient = _patientService.Get(patientId);
            var newPatient = model.ToModel(patient);
            newPatient.Id = patientId;
            var res= _patientService.Update(newPatient);
            return res.ToResponseModel();
        }
        #endregion
    }
}
