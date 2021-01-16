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
    public class SupportUserOrderController : ControllerBase
    {
        #region Fields
        private readonly ISupportUserOrderService _supportUserOrderService;
        private readonly SecurityHandler securityHandler = new SecurityHandler();
        #endregion

        #region Const
        public SupportUserOrderController(ISupportUserOrderService supportUserOrderService)
        {
            _supportUserOrderService = supportUserOrderService;
        }
        #endregion

        #region Actions
        [HttpPost("Add")]
        public ResponseModel Add([FromBody] SupportUserOrderModel model)
        {
            var supportUserId = securityHandler.GetUserId(User);
            var supportUserOrder = model.ToModel(supportUserId);
            var res = _supportUserOrderService.Create(supportUserOrder);
            return res.ToResponseModel();
        }
        [HttpGet("GetAll")]
        public ResponseModel GetAll()
        {
            var supportUserId = securityHandler.GetUserId(User);
            var res = _supportUserOrderService.GetBySupportUserId(supportUserId);
            return res.ToResponseModel();
        }
        #endregion
    }
}
