using Bipap.DAL.BaseRepository;
using Bipap.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bipap.Repository.IReposirories
{
    public interface IPatientRepository : IBaseRepository<Patient>
    {
        Task<IEnumerable<Patient>> GetPatientsByDoctorIdAsync(int doctorId);
        IEnumerable<Patient> GetPatientsByDoctorId(int doctorId);
        Patient GetByNationalCode(string nationalCode);
        Patient GetByMobileAndNationalCode(string mobile, string nationalCode);
        bool IsValidActiveCode(int userId, string activeCode);
        Patient GetPatientWithDoctorAndDevice(int patientId);
        Patient GetPatientWithDoctorByDeviceId(int deviceId);
    }
}
