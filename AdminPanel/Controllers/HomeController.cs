using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AdminPanel.Models;
using Bipap.DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using Bipap.Service.IServices;

namespace AdminPanel.Controllers
{
    [Authorize]
    public class HomeController : BaseController<BipapDbContext>
    {
        #region Fields
        private readonly ISupportUserOrderService _supportUserOrderService;
        private readonly IPatientService _patientService;
        private readonly IEndOfTreatmentService _endOfTreatmentService;
        private readonly IDeviceService _deviceService;
        #endregion
        public HomeController(BipapDbContext _context,
            IConfiguration config,
            IHostingEnvironment hostingEnvironment
            , ISupportUserOrderService supportUserOrderService
            , IEndOfTreatmentService endOfTreatmentService
            , IDeviceService deviceService
            , IPatientService patientService) : base(_context, config, hostingEnvironment)
        {
            _supportUserOrderService = supportUserOrderService;
            _patientService = patientService;
            _endOfTreatmentService = endOfTreatmentService;
            _deviceService = deviceService;
        }

        public IActionResult Index()
        {
            var model = new HomeIndexViewModel
            {
                SupportUserOrderCount = _supportUserOrderService.GetAll().Count(),
                PatientCount = _patientService.GetAll().Count(),
                EndOfTreatmentCount = _endOfTreatmentService.GetAll().Count(),
                DeviceCount = _deviceService.GetAll().Count()
            };
            return View(model);
        }
    }
}
