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
    public class SupportUserDeviceController : ControllerBase
    {
        #region Fields
        private readonly ISupportUserService _supportUserService;
        private readonly IDeviceService _deviceService;
        private readonly IPatientService _patientService;
        private readonly IPrescriptionService _prescriptionService;
        private readonly IDeviceTypeInformationService _deviceTypeInformationService;
        private readonly IEndOfTreatmentService _endOfTreatmentService;
        private readonly SecurityHandler securityHandler = new SecurityHandler();
        #endregion

        #region Const
        public SupportUserDeviceController(ISupportUserService supportUserService, IDeviceService deviceService
            , IPatientService patientService, IPrescriptionService prescriptionService
            , IDeviceTypeInformationService deviceTypeInformationService, IEndOfTreatmentService endOfTreatmentService)
        {
            _supportUserService = supportUserService;
            _deviceService = deviceService;
            _patientService = patientService;
            _prescriptionService = prescriptionService;
            _deviceTypeInformationService = deviceTypeInformationService;
            _endOfTreatmentService = endOfTreatmentService;
        }
        #endregion

        #region Actions
        [HttpGet("GetAllDevicesWithPrescriptions")]
        public ResponseModel GetAllDevicesWithPrescriptions()
        {
            var supportUserDeviceModelList = new List<SupportUserDeviceModel>();
            
            var supportUserId = securityHandler.GetUserId(User);
            var Devices = _deviceService.GetAllDeviceBySupportUserId(supportUserId);
            foreach (var device in Devices)
            {
                var supportUserDeviceModel = new SupportUserDeviceModel();
                var patientAndDoctor = _patientService.GetPatientWithDoctorByDeviceId(device.Id);
                
                var endOfTreatment = _endOfTreatmentService.GetByDeviceIdWithStatus(device.Id);

                supportUserDeviceModel.DeviceId = device.Id;
                supportUserDeviceModel.DeviceStatus = device.DeviceStatus?.Title;
                supportUserDeviceModel.DeviceType = device.DeviceType?.Title;
                supportUserDeviceModel.DeviceSerialNumber = device.SerialNumber;
                supportUserDeviceModel.DeliveryDate = device.DeliveryDate;

                supportUserDeviceModel.PatientFullName = patientAndDoctor?.FullName;
                supportUserDeviceModel.PatientMobile = patientAndDoctor?.Mobile;

                supportUserDeviceModel.DoctorFullName = patientAndDoctor?.Doctor?.FirstName + " " +
                    patientAndDoctor?.Doctor?.LastName;

                supportUserDeviceModel.HasEndOfTreatment = endOfTreatment == null ? false : true;
                supportUserDeviceModel.EndOfTreatmentId = endOfTreatment?.Id;
                supportUserDeviceModel.EndOfTreatmentStatus = endOfTreatment != null ? endOfTreatment.EndOfTreatmentStatus?.Title : "";
                var prescriptionList = new List<SupportUserDevicePrescription>();
                if (patientAndDoctor != null)
                {
                    var prescriptions = _prescriptionService.GetByPatientIdAndDeviceId(patientAndDoctor.Id, device.Id);
                    foreach (var prescription in prescriptions)
                    {
                        var deviceTypeIformation = _deviceTypeInformationService.
                        GetByDeviceTypeId((int)prescription.DeviceTypeId).ToList();
                        var rangeArray = prescription.Range.Split(',');
                        for (int i = 0; i < deviceTypeIformation.Count(); i++)
                        {
                            var newSupportUserDevicePrescription = new SupportUserDevicePrescription();
                            newSupportUserDevicePrescription.Title = deviceTypeIformation[i].Title;
                            newSupportUserDevicePrescription.Range = rangeArray[i];
                            newSupportUserDevicePrescription.RangeFrom = deviceTypeIformation[i].RangeFrom;
                            newSupportUserDevicePrescription.RangeTo = deviceTypeIformation[i].RangeTo;
                            newSupportUserDevicePrescription.Resolution = deviceTypeIformation[i].Resolution;
                            newSupportUserDevicePrescription.Unit = deviceTypeIformation[i].Unit;
                            prescriptionList.Add(newSupportUserDevicePrescription);
                        }
                    }
                    supportUserDeviceModel.Prescriptions = prescriptionList;
                }

                supportUserDeviceModelList.Add(supportUserDeviceModel);
            }
            return supportUserDeviceModelList.ToResponseModel();

        }
        [HttpGet("GetDevice")]
        public ResponseModel GetDevice(int deviceId)
        {
            var device = _deviceService.GetWithModeAndPatient(deviceId);
            return device.ToResponseModel();
        }
        #endregion
    }
}
