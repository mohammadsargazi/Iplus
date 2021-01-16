using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bipap.DAL.Models;
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
    public class EndOfTreatmentController : ControllerBase
    {
        #region Fileds
        private readonly IEndOfTreatmentService _endOfTreatmentService;
        private readonly IDeviceService _deviceService;
        private readonly SecurityHandler securityHandler = new SecurityHandler();
        #endregion

        #region Const
        public EndOfTreatmentController(IEndOfTreatmentService endOfTreatmentService, IDeviceService deviceService)
        {
            _endOfTreatmentService = endOfTreatmentService;
            _deviceService = deviceService;
        }
        #endregion

        #region Actions
        [HttpGet("Add")]
        public ResponseModel Add(int deviceId)
        {
            var supportUserId = securityHandler.GetUserId(User);
            var endOfTreatment = new EndOfTreatment
            {
                DeviceId = deviceId,
                EndOfTreatmentStatusId = 1,
                SupportUserId = supportUserId,
                Date = DateTime.Now
            };
            var res = _endOfTreatmentService.Create(endOfTreatment);
            return res.ToResponseModel();

        }
        #endregion
    }
}
