using Bipap.DAL.BaseRepository;
using Bipap.DAL.Models;
using System.Threading.Tasks;

namespace Bipap.Repository.IReposirories
{
    public interface IDoctorRepository : IBaseRepository<Doctor>
    {
        Task<Doctor> GetDoctorByMobileAndMedicalSystemCodeAsync(string mobile,string medicalSystemCode);
        Doctor GetDoctorByMobileAndMedicalSystemCode(string mobile, string medicalSystemCode);
        Task<Doctor> CheckActiveCodeAsync(string mobile, string activeCode);
        Doctor CheckActiveCode(string mobile, string activeCode);
    }
}
