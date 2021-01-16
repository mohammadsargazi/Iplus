using Bipap.DAL.BaseService;
using Bipap.DAL.Models;
using Bipap.DAL.UnitOfWork;
using Bipap.Repository.IReposirories;
using Bipap.Service.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bipap.Service.Services
{
    public class PrescriptionService : BaseService<Prescription>, IPrescriptionService
    {
        IUnitOfWork _unitOfWork;
        IPrescriptionRepository _prescriptionRepository;

        public PrescriptionService(IUnitOfWork unitOfWork, IPrescriptionRepository prescriptionRepository)
            : base(unitOfWork, prescriptionRepository)
        {
            _unitOfWork = unitOfWork;
            _prescriptionRepository = prescriptionRepository;
        }

        public async Task<IEnumerable<Prescription>> GetByPatientIdAsync(int patientId)
        {
            return await _prescriptionRepository.GetByPatientIdAsync(patientId);
        }
        public IEnumerable<Prescription> GetByPatientIdAndDeviceId(int patientId,int deviceId)
        {
            return _prescriptionRepository.GetByPatientIdAndDeviceId(patientId, deviceId);
        }

        public async Task<Prescription> GetPrescriptionWithPatientById(int prescriptionId)
        {
            return await _prescriptionRepository.GetPrescriptionWithPatientById(prescriptionId);
        }

        public Prescription GetLastPrescriptionByPatientId(int patientId)
        {
            return _prescriptionRepository.GetLastPrescriptionByPatientId(patientId);
        }

        public IEnumerable<Prescription> GetAllWithPatientId(int patientId)
        {
            return _prescriptionRepository.GetAllWithPatientId(patientId);
        }
    }
}
