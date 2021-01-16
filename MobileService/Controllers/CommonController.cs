using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bipap.Service.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobileService.Model;
using MobileService.Functionality;

namespace MobileService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommonController : ControllerBase
    {
        #region Fields
        private readonly ISettelmentStatusService _settelmentStatusService;
        #endregion

        #region Const
        public CommonController(ISettelmentStatusService settelmentStatusService)
        {
            _settelmentStatusService = settelmentStatusService;
        }
        #endregion

        #region Actions
        [HttpGet("GetAllSettelmentStatus")]
        public ResponseModel GetAllSettelmentStatus()
        {
            var res = _settelmentStatusService.GetAll();
            return res.ToResponseModel();
        }
        #endregion
    }
}
