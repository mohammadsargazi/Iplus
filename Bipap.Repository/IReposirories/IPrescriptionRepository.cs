using Bipap.DAL.BaseRepository;
using Bipap.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bipap.Repository.IReposirories
{
    public interface IPrescriptionRepository : IBaseRepository<Prescription>
    {
        Task<IEnumerable<Prescription>> GetByPatientIdAsync(int patientId);
        IEnumerable<Prescription> GetByPatientIdAndDeviceId(int patientId, int deviceId);
        Task<Prescription> GetPrescriptionWithPatientById(int prescriptionId);
        Prescription GetLastPrescriptionByPatientId(int patientId);
        IEnumerable<Prescription> GetAllWithPatientId(int patientId);
    }
}
