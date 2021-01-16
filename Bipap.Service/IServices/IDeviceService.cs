using Bipap.DAL.BaseService;
using Bipap.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bipap.Service.IServices
{
    public interface IDeviceService : IBaseService<Device>
    {
        Device GetWithModeAndPatient(int id);
        IEnumerable<Device> GetAllDeviceBySupportUserId(int supportUserId);
    }
}
