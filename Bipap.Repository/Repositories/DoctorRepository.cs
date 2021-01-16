using Bipap.DAL;
using Bipap.DAL.BaseRepository;
using Bipap.DAL.Models;
using Bipap.Repository.IReposirories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Bipap.Repository.Repositories
{
    public class DoctorRepository : BaseRepository<Doctor>, IDoctorRepository
    {
        private BipapDbContext BipapDbContext
        {
            get { return Context as BipapDbContext; }
        }
        public DoctorRepository(DbContext context)
              : base(context) { }

        public async Task<Doctor> GetDoctorByMobileAndMedicalSystemCodeAsync(string mobile, string medicalSystemCode)
        {
            return await BipapDbContext.Doctors
                .Where(x => x.Mobile == mobile && x.MedicalSystemCode == medicalSystemCode)
                .FirstOrDefaultAsync();
        }
        public Doctor GetDoctorByMobileAndMedicalSystemCode(string mobile, string medicalSystemCode)
        {
            return BipapDbContext.Doctors
                .Where(x => x.Mobile == mobile && x.MedicalSystemCode == medicalSystemCode)
                .FirstOrDefault();
        }

        public async Task<Doctor> CheckActiveCodeAsync(string mobile, string activeCode)
        {
            return await BipapDbContext.Doctors
                .Where(x => x.Mobile == mobile && x.ActiveCode == activeCode).FirstOrDefaultAsync();
        }
        public Doctor CheckActiveCode(string mobile, string activeCode)
        {
            return BipapDbContext.Doctors
                .Where(x => x.Mobile == mobile && x.ActiveCode == activeCode).FirstOrDefault();
        }
    }
}
