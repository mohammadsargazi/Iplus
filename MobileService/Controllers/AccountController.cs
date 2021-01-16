using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bipap.Service.IServices;
using Microsoft.AspNetCore.Mvc;
using MobileService.Functionality;
using MobileService.Model;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MobileService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        #region Fields
        private readonly ISupportUserService _supportUserService;
        private readonly IPatientService _patientService;
        private readonly SecurityHandler securityHandler = new SecurityHandler();
        #endregion

        #region Const
        public AccountController(ISupportUserService supportUserService, IPatientService patientService)
        {
            _supportUserService = supportUserService;
            _patientService = patientService;
        }
        #endregion

        #region SupportUserActions
        [AllowAnonymous]
        [HttpPost("SupportUserAuthenticate")]
        public ResponseModel SupportUserAuthenticate([FromBody] SupportUserViewModel model)
        {
            var supportUser = _supportUserService.GetByMobile(model.Mobile);
            if (supportUser == null)
                return supportUser.ToResponseModel();
            model = supportUser.ToModel();
            var res = securityHandler.GenerateToken(model);
            supportUser.ActiveCode = "1234";
            _supportUserService.Update(supportUser);
            return res.ToResponseModel();
        }
        [HttpPost("SupportUserValidateActiveCode")]
        public ResponseModel SupportUserValidateActiveCode([FromBody] SupportUserViewModel model)
        {
            var supportUserId = securityHandler.GetUserId(User);
            var isValid = _supportUserService.IsValidActiveCode(supportUserId, model.ActiveCode);
            if (!isValid)
                return new ResponseModel
                {
                    Data = null,
                    Message = "Active Code is not correct",
                    Status = false
                };
            return new ResponseModel
            {
                Data = null,
                Message = "Active Code is correct",
                Status = true
            };
        }
        #endregion

        #region PatientActions
        [AllowAnonymous]
        [HttpPost("PatientAuthenticate")]
        public ResponseModel PatientAuthenticate([FromBody] PatientViewMode model)
        {
            var patient = _patientService.GetByMobileAndNationalCode(model.Mobile, model.NationalCode);
            if (patient == null)
                return patient.ToResponseModel();
            model = patient.ToModel();
            var res = securityHandler.GenerateToken(model);
            //var jwtResult = _jwtAuthManager.GenerateTokens(patient.Id.ToString(), claims, DateTime.Now);
            patient.ActiveCode = "1234";
            _patientService.Update(patient);
            return res.ToResponseModel();
        }

        [HttpPost("PatientValidateActiveCode")]
        public ResponseModel PatientValidateActiveCode([FromBody] PatientViewMode model)
        {
            var patientId = securityHandler.GetUserId(User);
            var isValid = _patientService.IsValidActiveCode(patientId, model.ActiveCode);
            if (!isValid)
                return new ResponseModel
                {
                    Data = null,
                    Message = "Active Code is not correct",
                    Status = false
                };
            return new ResponseModel
            {
                Data = null,
                Message = "Active Code is correct",
                Status = true
            };
        }

        #endregion
    }
}
