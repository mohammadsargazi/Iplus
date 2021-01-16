using Bipap.DAL;
using Bipap.DAL.BaseRepository;
using Bipap.DAL.Models;
using Bipap.Repository.IReposirories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
namespace Bipap.Repository.Repositories
{
    public class DeviceTypeInformationRepository : BaseRepository<DeviceTypeInformation>, IDeviceTypeInformationRepository
    {
        private BipapDbContext BipapDbContext
        {
            get { return Context as BipapDbContext; }
        }
        public DeviceTypeInformationRepository(DbContext context)
              : base(context) { }

        public IEnumerable<DeviceTypeInformation> GetByDeviceTypeId(int deviceTypeId)
        {
            return BipapDbContext.DeviceTypeInformations
                .Where(x => x.DeviceTypeId == deviceTypeId)
                .ToList();
        }
    }
}
