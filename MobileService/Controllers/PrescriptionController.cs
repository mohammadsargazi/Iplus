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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionController : ControllerBase
    {
        #region Fileds
        private readonly IPrescriptionService _prescriptionService;
        private readonly IDeviceTypeService _deviceTypeServic;
        private readonly IDeviceTypeInformationService _deviceTypeInformationService;
        private readonly IPatientService _patientService;
        private readonly SecurityHandler securityHandler = new SecurityHandler();
        #endregion

        #region Const
        public PrescriptionController(IPrescriptionService prescriptionService, IPatientService patientService,
            IDeviceTypeService deviceTypeService, IDeviceTypeInformationService deviceTypeInformationService)
        {
            _prescriptionService = prescriptionService;
            _deviceTypeInformationService = deviceTypeInformationService;
            _deviceTypeServic = deviceTypeService;
            _patientService = patientService;
        }
        #endregion

        #region Actions
        [HttpGet("GetAll")]
        public ResponseModel GetAll()
        {
            var patientId = securityHandler.GetUserId(User);
            var PrescriptionModelList = new List<PrescriptionModel>();

            var prescriptions = _prescriptionService.GetAllWithPatientId(patientId);
            foreach (var prescription in prescriptions)
            {
                var newPrescriptionModel = new PrescriptionModel();
                newPrescriptionModel.DoctorFullName = prescription.Doctor?.FirstName 
                    + " " + prescription.Doctor?.LastName;
                newPrescriptionModel.SupportUserFullName = prescription?.Patient?.Device?.SupportUser?.FirstName
                    + " " + prescription?.Patient?.Device?.SupportUser?.LastName;
                newPrescriptionModel.SupportUserMobile = prescription?.Patient?.Device?.SupportUser?.Mobile;
                newPrescriptionModel.IssueDate = prescription.IssueDate;
                newPrescriptionModel.DeviceType = prescription.DeviceType?.Title;
                newPrescriptionModel.CompletionDate = prescription.CompletionDate;
                newPrescriptionModel.DurationOfTreatment = prescription.DurationOfTreatment;
                newPrescriptionModel.PrescriptionStatus = prescription.PrescriptionStatus?.Title;
                var newPrescriptionInformationModelList = new List<PrescriptionInformationModel>();

                var deviceTypeIformation = _deviceTypeInformationService.
                    GetByDeviceTypeId((int)prescription.DeviceTypeId).ToList();
                var rangeArray = prescription.Range.Split(',');
                for (int i = 0; i < deviceTypeIformation.Count(); i++)
                {
                    var newPrescriptionInformationModel = new PrescriptionInformationModel();
                    newPrescriptionInformationModel.Title = deviceTypeIformation[i].Title;
                    newPrescriptionInformationModel.Range = rangeArray[i];
                    newPrescriptionInformationModel.RangeFrom = deviceTypeIformation[i].RangeFrom;
                    newPrescriptionInformationModel.RangeTo = deviceTypeIformation[i].RangeTo;
                    newPrescriptionInformationModel.Resolution = deviceTypeIformation[i].Resolution;
                    newPrescriptionInformationModel.Unit = deviceTypeIformation[i].Unit;
                    newPrescriptionInformationModelList.Add(newPrescriptionInformationModel);
                }
                newPrescriptionModel.PrescriptionInformationModels = newPrescriptionInformationModelList;
                PrescriptionModelList.Add(newPrescriptionModel);
            }
            return PrescriptionModelList.ToResponseModel();
        }
        [HttpGet("Get")]
        public ResponseModel Get(int id)
        {
            var patientId = securityHandler.GetUserId(User);
            var res = _prescriptionService.Get(id);
            return res.ToResponseModel();
        }
        #endregion
    }
}
