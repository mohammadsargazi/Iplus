//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Bipap.DAL.Models;
//using Bipap.Service.IServices;
//using Microsoft.AspNetCore.Mvc;
//using SupportUserPanel.Functionality;

//namespace SupportUserPanel.Controllers
//{
//    public class PatientController : Controller
//    {
//        #region Fields
//        private readonly IPatientService _patientService;
//        private readonly ISupportUserDeviceService _supportUserDeviceService;
//        private readonly IDeviceService _deviceService;
//        #endregion

//        #region Const
//        public PatientController(IPatientService patientService, ISupportUserDeviceService supportUserDeviceService
//            ,IDeviceService deviceService)
//        {
//            _patientService = patientService;
//            _supportUserDeviceService = supportUserDeviceService;
//            _deviceService = deviceService;
//        }
//        #endregion

//        #region Actions
//        public IActionResult List()
//        {
//            var userId = HttpContextMethod.GetCurrentUserId(User);
//            var deviceList = new List<Device>();
//            var supportUserDevices = _supportUserDeviceService.GetBySupportUserIdWithDevice(userId);
//            foreach (var item in supportUserDevices)
//            {
//                var device = _deviceService.GetWithModeAndPatient((int)item.DeviceId);
//                deviceList.Add(device);
//            }
//            return View(deviceList);
//        }
//        #endregion
//    }
//}
