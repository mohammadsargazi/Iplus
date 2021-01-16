using Bipap.DAL.BaseService;
using Bipap.DAL.Models;
using System.Collections.Generic;

namespace Bipap.Service.IServices
{
    public interface IDeviceTypeInformationService : IBaseService<DeviceTypeInformation>
    {
        IEnumerable<DeviceTypeInformation> GetByDeviceTypeId(int deviceTypeId);
    }
}
