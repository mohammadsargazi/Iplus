using Bipap.DAL.BaseService;
using Bipap.DAL.Models;
using System.Threading.Tasks;

namespace Bipap.Service.IServices
{
    public interface IEndOfTreatmentService : IBaseService<EndOfTreatment>
    {
        EndOfTreatment GetByDeviceIdWithStatus(int deviceId);
    }
}
