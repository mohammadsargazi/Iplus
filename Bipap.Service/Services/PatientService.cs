using Bipap.DAL.BaseService;
using Bipap.DAL.Models;
using Bipap.DAL.UnitOfWork;
using Bipap.Repository.IReposirories;
using Bipap.Service.IServices;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bipap.Service.Services
{
    public class PatientService : BaseService<Patient>, IPatientService
    {
        IUnitOfWork _unitOfWork;
        IPatientRepository _patientRepository;

        public PatientService(IUnitOfWork unitOfWork, IPatientRepository patientRepository)
            : base(unitOfWork, patientRepository)
        {
            _unitOfWork = unitOfWork;
            _patientRepository = patientRepository;
        }


        public async Task<IEnumerable<Patient>> GetPatientsByDoctorIdAsync(int doctorId)
        {
            return await _patientRepository.GetPatientsByDoctorIdAsync(doctorId);
        }
        public IEnumerable<Patient> GetPatientsByDoctorId(int doctorId)
        {
            return _patientRepository.GetPatientsByDoctorId(doctorId);
        }
        public Patient GetByNationalCode(string nationalCode)
        {
            return _patientRepository.GetByNationalCode(nationalCode);
        }

        public Patient GetByMobileAndNationalCode(string mobile, string nationalCode)
        {
            return _patientRepository.GetByMobileAndNationalCode(mobile, nationalCode);
        }

        public bool IsValidActiveCode(int userId, string activeCode)
        {
            return _patientRepository.IsValidActiveCode(userId, activeCode);
        }

        public Patient GetPatientWithDoctorAndDevice(int patientId)
        {
            return _patientRepository.GetPatientWithDoctorAndDevice(patientId);
        }

        public Patient GetPatientWithDoctorByDeviceId(int deviceId)
        {
            return _patientRepository.GetPatientWithDoctorByDeviceId(deviceId);
        }
    }
}
