using Bipap.DAL.BaseService;
using Bipap.DAL.Models;
using Bipap.DAL.UnitOfWork;
using Bipap.Repository.IReposirories;
using Bipap.Service.IServices;
namespace Bipap.Service.Services
{
    public class DeviceTypeService : BaseService<DeviceType>, IDeviceTypeService
    {
        IUnitOfWork _unitOfWork;
        IDeviceTypeRepository _deviceTypeRepository;

        public DeviceTypeService(IUnitOfWork unitOfWork, IDeviceTypeRepository deviceTypeRepository)
            : base(unitOfWork, deviceTypeRepository)
        {
            _unitOfWork = unitOfWork;
            _deviceTypeRepository = deviceTypeRepository;
        }
    }
}
