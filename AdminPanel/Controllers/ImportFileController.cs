using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AdminPanel.Functionality;
using Bipap.DAL.Models;
using Bipap.Service.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using AdminPanel.Models;

namespace AdminPanel.Controllers
{
    public class ImportFileController : Controller
    {
        private readonly ExcelFunctionality excelFunctionality = new ExcelFunctionality();
        private readonly IDeviceService _deviceService;

        public ImportFileController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ImportDevice()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ImportDevice(IList<IFormFile> files)
        {
            var filesss = Request.Form.Files;
            var filePath = Path.GetTempFileName();
            var deviceList = new List<Device>();
            foreach (var file in filesss)
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyToAsync(stream);
                    //var streamFile = file.OpenReadStream();
                    var fileData = excelFunctionality.GetDataFromCSVFile(stream);
                    var dtDevice = fileData.ToDataTable();
                    var deviceModelList = dtDevice.ToDeviceModel();
                    deviceList.AddRange(deviceModelList);
                }
            }
            foreach (var device in deviceList)
            {
                _deviceService.Create(device);
            }
            return View();
        }


    }
}
