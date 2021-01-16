using Bipap.DAL.BaseService;
using Bipap.DAL.Models;
using System.Threading.Tasks;

namespace Bipap.Service.IServices
{
    public interface IDoctorService : IBaseService<Doctor>
    {
        Task<Doctor> GetDoctorByMobileAndMedicalSystemCodeAsync(string mobile, string medicalSystemCode);
        Task<Doctor> CheckActiveCodeAsync(string mobile,string activeCode);
        Doctor GetDoctorByMobileAndMedicalSystemCode(string mobile, string medicalSystemCode);
        Doctor CheckActiveCode(string mobile, string activeCode);
    }
}
