using Bipap.DAL;
using Bipap.DAL.BaseRepository;
using Bipap.DAL.Models;
using Bipap.Repository.IReposirories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bipap.Repository.Repositories
{
    public class PrescriptionRepository : BaseRepository<Prescription>, IPrescriptionRepository
    {
        private BipapDbContext BipapDbContext
        {
            get { return Context as BipapDbContext; }
        }
        public PrescriptionRepository(DbContext context)
              : base(context) { }

        public async Task<IEnumerable<Prescription>> GetByPatientIdAsync(int patientId)
        {
            return await BipapDbContext.Prescriptions
                .Where(x => x.PatientId == patientId)
                .Include(x => x.DeviceType).ToListAsync();
        }
        public IEnumerable<Prescription> GetByPatientIdAndDeviceId(int patientId,int deviceId)
        {
            return  BipapDbContext.Prescriptions
                .Where(x => x.PatientId == patientId && x.Patient.DeviceId==deviceId)
                .Include(x => x.DeviceType).ToList();
        }

        public async Task<Prescription> GetPrescriptionWithPatientById(int prescriptionId)
        {
            return await BipapDbContext.Prescriptions
                .Where(x => x.Id == prescriptionId)
                .Include(x => x.Patient)
                .FirstOrDefaultAsync();
        }

        public Prescription GetLastPrescriptionByPatientId(int patientId)
        {
            return BipapDbContext.Prescriptions
                .Where(x => x.PatientId == patientId)
                .OrderByDescending(x => x.IssueDate)
                .Include(x=>x.PrescriptionStatus)
                .FirstOrDefault();
        }

        public IEnumerable<Prescription> GetAllWithPatientId(int patientId)
        {
            return BipapDbContext.Prescriptions
                .Where(x => x.PatientId == patientId)
                .Include(x=>x.PrescriptionStatus)
                .Include(x=>x.DeviceType)
                .Include(x=>x.Patient.Doctor)
                .Include(x=>x.Patient.Device)
                .Include(x=>x.Patient.Device.SupportUser)
                .ToList();
        }
    }
}
