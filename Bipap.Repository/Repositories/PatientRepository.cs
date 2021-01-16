using Bipap.DAL;
using Bipap.DAL.BaseRepository;
using Bipap.DAL.Models;
using Bipap.Repository.IReposirories;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bipap.Repository.Repositories
{
    public class PatientRepository : BaseRepository<Patient>, IPatientRepository
    {
        private BipapDbContext BipapDbContext
        {
            get { return Context as BipapDbContext; }
        }
        public PatientRepository(DbContext context)
              : base(context) { }

        public async Task<IEnumerable<Patient>> GetPatientsByDoctorIdAsync(int doctorId)
        {
            return await BipapDbContext.Patients.Where(x => x.DoctorId == doctorId).ToListAsync();
        }
        public IEnumerable<Patient> GetPatientsByDoctorId(int doctorId)
        {
            return BipapDbContext.Patients.Where(x => x.DoctorId == doctorId).ToList();
        }

        public Patient GetByNationalCode(string nationalCode)
        {
            return BipapDbContext.Patients
                 .Where(x => x.NantionalCode == nationalCode)
                 .FirstOrDefault();
        }

        public Patient GetByMobileAndNationalCode(string mobile, string nationalCode)
        {
            return BipapDbContext.Patients
                .Where(x => x.Mobile == mobile && x.NantionalCode == nationalCode)
                .FirstOrDefault();
        }

        public bool IsValidActiveCode(int userId, string activeCode)
        {
            return BipapDbContext.Patients
                .Any(x => x.Id == userId && x.ActiveCode == activeCode);
        }

        public Patient GetPatientWithDoctorAndDevice(int patientId)
        {
            return BipapDbContext.Patients
                .Where(x => x.Id == patientId)
                .Include(x => x.Doctor)
                .Include(x => x.Device)
                .Include(x => x.Device.DeviceType)
                .Include(x=>x.Device.SupportUser)
                .FirstOrDefault();
        }

        public Patient GetPatientWithDoctorByDeviceId(int deviceId)
        {
            return BipapDbContext.Patients
                .Where(x => x.DeviceId == deviceId)
                .Include(x => x.Doctor)
                .FirstOrDefault();
        }
    }
}
