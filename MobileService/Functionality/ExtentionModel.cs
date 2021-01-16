using Bipap.DAL;
using Bipap.DAL.Models;
using MobileService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileService.Functionality
{
    public static class ExtentionModel
    {
        #region Response
        public static ResponseModel ToResponseModel(this object model)
        {
            if (model == null)
                return new ResponseModel
                {
                    Status = false,
                    Message = "Model not fond",
                    Data = model
                };
            return new ResponseModel
            {
                Status = true,
                Message = null,
                Data = model
            };
        }
        public static ResponseModel ToResponseModel(this ResultMessage model)
        {
            return new ResponseModel
            {
                Status = model.IsSubmited,
                Message = model.Message,
                Data = null
            };
        }
        #endregion

        #region Dashbord
        public static PatientInformationModel ToModel(this Patient patient, Prescription prescription)
        {
            if (patient == null)
                return null;
            return new PatientInformationModel
            {
                PatientFullName = patient.FullName,
                PatientNationalCode = patient.NantionalCode,
                DeviceType = patient.Device?.DeviceType?.Title,
                DeviceNumber = patient.Device?.SerialNumber,
                DoctorFullName = patient.Doctor?.FirstName + " " + patient.Doctor?.LastName,
                LastPrescriptionDate = prescription == null ? (DateTime?)null : prescription.IssueDate,
                LastPrescriptionStatus = prescription == null ? null : prescription.PrescriptionStatus?.Title,
                SupportUserFullName = patient.Device?.SupportUser?.FirstName + " " + patient.Device?.SupportUser?.FirstName,
                SupportUserMobile = patient.Device?.SupportUser?.Mobile
            };
        }
        #endregion

        #region Account
        public static PatientViewMode ToModel(this Patient model)
        {
            if (model == null)
                return null;
            return new PatientViewMode
            {
                Id = model.Id,
                ActiveCode = model.ActiveCode,
                Mobile = model.Mobile,
                NationalCode = model.NantionalCode,
            };
        }
        public static SupportUserViewModel ToModel(this SupportUser model)
        {
            if (model == null)
                return null;
            return new SupportUserViewModel
            {
                Id = model.Id,
                ActiveCode = model.ActiveCode,
                Mobile = model.Mobile,
            };
        }
        #endregion

        #region Patient
        public static Patient ToModel(this PatientModel model, Patient patient)
        {
            if (model == null)
                return patient;
            patient.FullName = model.FullName == null ? patient.FullName : model.FullName;
            patient.Age = model.Age == null ? patient.Age : (int)model.Age;
            patient.Email = model.Email == null ? patient.Email : model.Email;
            patient.GenderId = model.GenderId == null ? patient.GenderId : model.GenderId;
            patient.Address = model.Address == null ? patient.Address : model.Address;
            patient.DateOfBirth = model.DateOfBirth == null ? patient.DateOfBirth : (DateTime)model.DateOfBirth;
            return patient;
        }
        #endregion

        #region SupportUserOrder
        public static SupportUserOrder ToModel(this SupportUserOrderModel model,int supportUserId)
        {
            if (model == null)
                return null;
            return new SupportUserOrder
            {
                Date=DateTime.Now,
                Count = model.Count,
                Price = model.Price,
                Description = model.Description,
                SettelmentStatusId = model.SettelmentStatusId,
                SupportUserId=supportUserId
            };
        }
        #endregion
    }
}
