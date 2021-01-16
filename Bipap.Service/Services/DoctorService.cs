using Bipap.DAL.BaseService;
using Bipap.DAL.Models;
using Bipap.DAL.UnitOfWork;
using Bipap.Repository.IReposirories;
using Bipap.Service.IServices;
using System.Threading.Tasks;

namespace Bipap.Service.Services
{
    public class DoctorService : BaseService<Doctor>, IDoctorService
    {
        IUnitOfWork _unitOfWork;
        IDoctorRepository _doctorRepository;

        public DoctorService(IUnitOfWork unitOfWork, IDoctorRepository doctorRepository)
            : base(unitOfWork, doctorRepository)
        {
            _unitOfWork = unitOfWork;
            _doctorRepository = doctorRepository;
        }


        public async Task<Doctor> CheckActiveCodeAsync(string mobile, string activeCode)
        {
            return await _doctorRepository.CheckActiveCodeAsync(mobile, activeCode);
        }

        public async Task<Doctor> GetDoctorByMobileAndMedicalSystemCodeAsync(string mobile, string medicalSystemCode)
        {
            return await _doctorRepository.GetDoctorByMobileAndMedicalSystemCodeAsync(mobile, medicalSystemCode);
        }
        public Doctor CheckActiveCode(string mobile, string activeCode)
        {
            return  _doctorRepository.CheckActiveCode(mobile, activeCode);
        }

        public Doctor GetDoctorByMobileAndMedicalSystemCode(string mobile, string medicalSystemCode)
        {
            return  _doctorRepository.GetDoctorByMobileAndMedicalSystemCode(mobile, medicalSystemCode);
        }
    }
}
