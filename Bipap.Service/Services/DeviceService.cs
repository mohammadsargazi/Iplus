using Bipap.DAL.BaseService;
using Bipap.DAL.Models;
using Bipap.DAL.UnitOfWork;
using Bipap.Repository.IReposirories;
using Bipap.Service.IServices;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bipap.Service.Services
{
  public  class DeviceService : BaseService<Device>, IDeviceService
    {
        IUnitOfWork _unitOfWork;
        IDeviceRepository _deviceRepository;

        public DeviceService(IUnitOfWork unitOfWork, IDeviceRepository deviceRepository)
            : base(unitOfWork, deviceRepository)
        {
            _unitOfWork = unitOfWork;
            _deviceRepository = deviceRepository;
        }

        public IEnumerable<Device> GetAllDeviceBySupportUserId(int supportUserId)
        {
            return _deviceRepository.GetAllDeviceBySupportUserId(supportUserId);
        }

        public Device GetWithModeAndPatient(int id)
        {
            return _deviceRepository.GetWithModeAndPatient(id);
        }
    }
}
