using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bipap.Service.Functionality;
using Bipap.Service.IServices;
using Microsoft.AspNetCore.Mvc;
using UI.Functionality;
using UI.Models;

namespace UI.Controllers
{
    public class ReportController : Controller
    {
        #region Fields
        private readonly IStepOneModuleService _stepOneModuleService;
        private readonly IFileService _fileService;
        private readonly IDoctorService _doctorService;
        private readonly HtmlToPdfConverter convertor = new HtmlToPdfConverter();
        private readonly AnalizeModule _analizeModule = new AnalizeModule();
        #endregion

        #region Const
        public ReportController(IStepOneModuleService stepOneModuleService, IFileService fileService,IDoctorService doctorService)
        {
            _stepOneModuleService = stepOneModuleService;
            _fileService = fileService;
            _doctorService = doctorService;
        }
        #endregion
        public IActionResult Index(string stepOneModuleIds, int fileId)
        {
            ViewBag.SelectedSessions = stepOneModuleIds.ToString();
            var file = _fileService.GetFileWithPatientById(fileId);
            //var stepOneModule = _stepOneModuleService.GetStepOneModuleWithFile(stepOneModuleId);
            //var moduleStepTwoResult = HelperMethod.AnalizeStepOneModule(stepOneModule);
            //ViewBag.File = file;
            ViewBag.Patient = file.Patient;
            //ViewBag.StepOneModule = stepOneModule;
            //ViewBag.ModuleStepTwoResult = moduleStepTwoResult;
            //ViewBag.GraphSummaryModel = new GraphSummaryViewModel { Flow = stepOneModule.Flow.ToString(), Leak = stepOneModule.Leak.ToString(), Pressure = stepOneModule.Pressure.ToString(), TidalVolume = moduleStepTwoResult.tidal_volume.ToString() };
            return View();
        }
        public IActionResult Statics(int stepOneModuleId)
        {
            var model = _stepOneModuleService.GetStepOneModuleWithFile(stepOneModuleId);
            var sessionStepOneModel = model.ToModel();
            var moduleStepTwoResult = _analizeModule.Analize(sessionStepOneModel);//AnalizeModule.Analize(model);
            ViewBag.File = model.File;
            return View(moduleStepTwoResult);
        }
        public IActionResult GraphSummary(int stepOneModuleId)
        {
            var stepOneModule = _stepOneModuleService.GetStepOneModuleWithFile(stepOneModuleId);
            var sessionStepOneModel = stepOneModule.ToModel();
            var moduleStepTwoResult = _analizeModule.Analize(sessionStepOneModel);// AnalizeModule.Analize(stepOneModule);
            ViewBag.File = stepOneModule.File;
            var tidalvolume = string.Join(",", moduleStepTwoResult?.tidal_volume);
            var model = new GraphSummaryViewModel { Flow = stepOneModule?.Flow.ToString(), Leak = stepOneModule?.Leak.ToString(), Pressure = stepOneModule?.Pressure.ToString(), TidalVolume = tidalvolume };
            return View(model);



        }
        public IActionResult ExportPDF(string stepOneModuleIds, string fileId)
        {
            var stepOneModuleIdList = stepOneModuleIds.Split(',').ToList();
            var fileIdInt = Convert.ToInt32(fileId);
            var file = _fileService.GetFileWithPatientById(fileIdInt);
            var moduleStepTwoResultList = new List<ModuleStepTwoResult>();
            foreach (var item in stepOneModuleIdList)
            {
                var stepOneModuleId = Convert.ToInt32(item);
                var stepOneModule = _stepOneModuleService.GetStepOneModuleWithFile(stepOneModuleId);
                var sessionStepOneModel = stepOneModule.ToModel();
                var moduleStepTwoResult = _analizeModule.Analize(sessionStepOneModel);//AnalizeModule.Analize(stepOneModule);
                moduleStepTwoResultList.Add(moduleStepTwoResult);
            }
            var currentUserId = HttpContextMethod.GetCurrentUserId(User);
            var doctor = _doctorService.Get(currentUserId);
            var reportModel = moduleStepTwoResultList.ToReportModel(file.Patient, doctor);
            //var html = reportModel.GenerateHtml();
            //var htmlStr = "<!DOCTYPE html><html><head><meta charset ='utf-8'/>" +
            //    "<script src='~/lib/jquery/dist/jquery.min.js'>" +
            //    "</script><script src='~/Plugins/Hichart/newJS/highstock.js'>" +
            //    "</script><script src='~/Plugins/Hichart/newJS/exporting.js'>" +
            //    "</script><script src='~/Plugins/Hichart/chartfunctions.js'></script>" +
            //    "</head><body dir='rtl' style='font-family:bnazanin;'>" + html + "</body></html>";
            //var res = convertor.GeneratePdf(htmlStr);
            //return File(res, "application/pdf", "DownloadName.pdf");
            return View(reportModel);
        }

        //[HttpPost]
        //[ValidateInput(false)]
        //public ActionResult ExportPdf(string html, int meetingId)
        //{
        //    //var newHtml = "@media print{.page -break { display: block;page -break-before:always;}}" + html;
        //    string pdfPath = AppDomain.CurrentDomain.BaseDirectory + "PdfFile/" + meetingId.ToString() + ".pdf";
        //    if (System.IO.File.Exists(pdfPath))
        //        System.IO.File.Delete(pdfPath);
        //    var htmlStr = "<!DOCTYPE html><html><head><meta charset ='utf-8'/><link href='~/Content/meeting/main.css' rel='stylesheet' type='text/css'></head><body dir='rtl' style='font-family:bnazanin;'>" + html + "</body></html>";
        //    var convertor = new HtmlToPdfConverter();
        //    //convertor.CustomWkHtmlArgs = "  --font-family: bnazanin;src: url('~/fonts/BNazanin.woff'); ";
        //    //convertor.CustomWkHtmlArgs= "@media print{.page -break {display: block;page -break-before:always; }     }"
        //    var res = convertor.GeneratePdf(htmlStr);
        //    System.IO.File.WriteAllBytes(pdfPath, res);
        //    return Json(new { path = pdfPath }, JsonRequestBehavior.AllowGet);
        //}
    }
}
