using Bipap.DAL.BaseService;
using Bipap.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bipap.Service.IServices
{
    public interface IPrescriptionService : IBaseService<Prescription>
    {
        Task<IEnumerable<Prescription>> GetByPatientIdAsync(int patientId);
        IEnumerable<Prescription> GetByPatientIdAndDeviceId(int patientId, int deviceId);
        Task<Prescription> GetPrescriptionWithPatientById(int prescriptionId);
        Prescription GetLastPrescriptionByPatientId(int patientId);
        IEnumerable<Prescription> GetAllWithPatientId(int patientId);
    }
}
