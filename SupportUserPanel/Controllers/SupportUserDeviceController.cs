//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Bipap.Service.IServices;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using SupportUserPanel.Functionality;

//namespace SupportUserPanel.Controllers
//{
//    [Authorize]
//    public class SupportUserDeviceController : Controller
//    {
//        #region Fields
//        private readonly ISupportUserDeviceService _supportUserDeviceService;
//        #endregion

//        #region Const
//        public SupportUserDeviceController(ISupportUserDeviceService supportUserDeviceService)
//        {
//            _supportUserDeviceService = supportUserDeviceService;
//        }
//        #endregion

//        #region Actions
//        public IActionResult List()
//        {
//            var userId = HttpContextMethod.GetCurrentUserId(User);
//            var model = _supportUserDeviceService.GetBySupportUserIdWithDevice(userId);
//            return View(model);
//        }
//        #endregion
//    }
//}
