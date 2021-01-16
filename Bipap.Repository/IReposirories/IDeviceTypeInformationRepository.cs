using Bipap.DAL.BaseRepository;
using Bipap.DAL.Models;
using System.Collections.Generic;

namespace Bipap.Repository.IReposirories
{
    public interface IDeviceTypeInformationRepository : IBaseRepository<DeviceTypeInformation>
    {
        IEnumerable<DeviceTypeInformation> GetByDeviceTypeId(int deviceTypeId);
    }
}
