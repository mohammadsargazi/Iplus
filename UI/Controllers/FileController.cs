using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Bipap.DAL.Extentions;
using Bipap.Service.Functionality;
using Bipap.Service.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UI.Functionality;

namespace UI.Controllers
{
    public class FileController : Controller
    {
        #region Fields
        private readonly IFileService _fileService;
        private readonly IStepOneModuleService _stepOneModuleService;
        private readonly Module _module = new Module();
        #endregion
        #region Const
        public FileController(IFileService fileService, IStepOneModuleService stepOneModuleService)
        {
            _fileService = fileService;
            _stepOneModuleService = stepOneModuleService;
        }
        #endregion
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult List()
        {
            var models = _fileService.GetByPatientId(1);
            return View(models);
        }
        public IActionResult Add(int patientId)
        {
            ViewBag.PatientId = patientId;
            return View();
        }
        public IActionResult Detaile(int fileId)
        {
            ViewBag.File = _fileService.Get(fileId);
            var model = _stepOneModuleService.GetByFileId(fileId);
            return View(model);
        }
        public async Task<IActionResult> Upload(IList<IFormFile> files)
        {
            try
            {
                var patientId = HttpContext.Request.Query["patientId"].ToString();
                var newFileModel = new Bipap.DAL.Models.File();
                newFileModel.PatientId = Convert.ToInt32(patientId);
                var result = _fileService.Create(newFileModel);
                var stepOneModuleList = new List<Bipap.DAL.Models.StepOneModule>();
                foreach (var file in files)
                {
                    if (file.Length > 2000)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            var fileBytes = ms.ToArray();
                            var stepOneModule = _module.GetStepOneModule(fileBytes, file.Length, result.Id);
                            stepOneModuleList.Add(stepOneModule);
                            await _stepOneModuleService.CreateAsync(stepOneModule);
                        }
                    }

                }
                var dayCount = stepOneModuleList.GetDayCountFromStepOneModules();// ExtentionModel.GetDayCountFromStepOneModules();
                var fileModel = _fileService.Get(result.Id);
                fileModel.SessionCount = stepOneModuleList.Count();
                fileModel.DayCount = dayCount;
                //fileModel.FileUploadTypeId = 1;
                _fileService.Update(fileModel);
                return Json(new { res = true, message = "فایل های انتخاب شده با موفقیت ذخیره شد." });
            }
            catch (Exception ex)
            {
                return Json(new { res = false, message = "مشکلی در هنگام ذخیره شدن فایل ها بوجود آمده است." });
            }
        }
    }
}
