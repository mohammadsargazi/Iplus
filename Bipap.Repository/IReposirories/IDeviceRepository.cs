using Bipap.DAL.BaseRepository;
using Bipap.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bipap.Repository.IReposirories
{
    public interface IDeviceRepository : IBaseRepository<Device>
    {
        Device GetWithModeAndPatient(int id);
        IEnumerable<Device> GetAllDeviceBySupportUserId(int supportUserId);
    }
}
