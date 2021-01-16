using Bipap.DAL;
using Bipap.DAL.BaseRepository;
using Bipap.DAL.Models;
using Bipap.Repository.IReposirories;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Bipap.Repository.Repositories
{
   public class DeviceTypeRepository : BaseRepository<DeviceType>, IDeviceTypeRepository
    {
        private BipapDbContext BipapDbContext
        {
            get { return Context as BipapDbContext; }
        }
        public DeviceTypeRepository(DbContext context)
              : base(context) { }
    }
}
