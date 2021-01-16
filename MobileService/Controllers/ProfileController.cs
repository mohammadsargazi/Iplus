using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bipap.DAL.Models;
using Bipap.Service.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobileService.Functionality;
using MobileService.Model;


namespace MobileService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        #region Fields
        private readonly ISupportUserService _supportUserService;
        private readonly SecurityHandler securityHandler = new SecurityHandler();
        #endregion

        #region Const
        public ProfileController(ISupportUserService supportUserService)
        {
            _supportUserService = supportUserService;
        }
        #endregion

        #region Actions
        [HttpPost("SupportUserUpdate")]
        public ResponseModel SupportUserUpdate([FromBody] SupportUser model)
        {
            var supportUserId = securityHandler.GetUserId(User);
            model.Id = supportUserId;
            var res = _supportUserService.Update(model);
            return res.ToResponseModel();
        }
        [HttpGet("GetSupportUser")]
        public ResponseModel GetSupportUser()
        {
            var supportUserId = securityHandler.GetUserId(User);
            var res = _supportUserService.Get(supportUserId);
            return res.ToResponseModel();
        }

        #endregion
    }
}
