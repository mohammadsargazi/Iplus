using Bipap.DAL.BaseRepository;
using Bipap.DAL.Models;
using System.Threading.Tasks;
namespace Bipap.Repository.IReposirories
{
    public interface IEndOfTreatmentRepository : IBaseRepository<EndOfTreatment>
    {
        EndOfTreatment GetByDeviceIdWithStatus(int deviceId);
    }
}
