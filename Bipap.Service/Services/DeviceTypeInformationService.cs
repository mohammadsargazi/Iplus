using Bipap.DAL.BaseService;
using Bipap.DAL.Models;
using Bipap.DAL.UnitOfWork;
using Bipap.Repository.IReposirories;
using Bipap.Service.IServices;
using System.Collections.Generic;

namespace Bipap.Service.Services
{
    public class DeviceTypeInformationService : BaseService<DeviceTypeInformation>, IDeviceTypeInformationService
    {
        IUnitOfWork _unitOfWork;
        IDeviceTypeInformationRepository _deviceTypeInformationRepository;

        public DeviceTypeInformationService(IUnitOfWork unitOfWork, IDeviceTypeInformationRepository deviceTypeInformationRepository)
            : base(unitOfWork, deviceTypeInformationRepository)
        {
            _unitOfWork = unitOfWork;
            _deviceTypeInformationRepository = deviceTypeInformationRepository;
        }

        public IEnumerable<DeviceTypeInformation> GetByDeviceTypeId(int deviceTypeId)
        {
            return _deviceTypeInformationRepository.GetByDeviceTypeId(deviceTypeId);
        }
    }
}
