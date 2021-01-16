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
    public class FileController : ControllerBase
    {
        #region Fields
        private readonly SecurityHandler securityHandler = new SecurityHandler();
        private readonly IFileService _fileService;
        private readonly IStepOneModuleService _stepOneModuleService;
        #endregion

        #region Const
        public FileController(IFileService fileService,IStepOneModuleService stepOneModuleService)
        {
            _fileService = fileService;
            _stepOneModuleService = stepOneModuleService;
        }
        #endregion

        #region Actions
        [HttpGet("Get")]
        public ResponseModel Get()
        {
            var patientId = securityHandler.GetUserId(User);
            var res= _fileService.GetByPatientId(patientId);
            return res.ToResponseModel();
        }
       
        #endregion
    }
}
