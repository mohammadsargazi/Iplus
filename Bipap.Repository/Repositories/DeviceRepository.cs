using Bipap.DAL;
using Bipap.DAL.BaseRepository;
using Bipap.DAL.Models;
using Bipap.Repository.IReposirories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bipap.Repository.Repositories
{
    public class DeviceRepository : BaseRepository<Device>, IDeviceRepository
    {
        private BipapDbContext BipapDbContext
        {
            get { return Context as BipapDbContext; }
        }
        public DeviceRepository(DbContext context)
              : base(context) { }

        public Device GetWithModeAndPatient(int id)
        {
            return BipapDbContext.Devices
                .Where(x => x.Id == id)
                .Include(x => x.DeviceType)
                .Include(x=>x.DeviceStatus).FirstOrDefault();
        }

        public IEnumerable<Device> GetAllDeviceBySupportUserId(int supportUserId)
        {
            return BipapDbContext.Devices
                .Where(x => x.SupportUserId == supportUserId)
                .Include(x => x.SupportUser)
                .Include(x => x.DeviceStatus)
                .Include(x => x.DeviceType)
                .ToList();
        }
    }
}
