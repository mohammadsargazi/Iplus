using Bipap.DAL;
using Bipap.DAL.BaseRepository;
using Bipap.DAL.Models;
using Bipap.Repository.IReposirories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Bipap.Repository.Repositories
{
    public class EndOfTreatmentRepository : BaseRepository<EndOfTreatment>, IEndOfTreatmentRepository
    {
        private BipapDbContext BipapDbContext
        {
            get { return Context as BipapDbContext; }
        }
        public EndOfTreatmentRepository(DbContext context)
              : base(context) { }

        public EndOfTreatment GetByDeviceIdWithStatus(int deviceId)
        {
            return BipapDbContext.EndOfTreatments
                .Where(x => x.DeviceId == deviceId)
                .Include(x => x.EndOfTreatmentStatus)
                .FirstOrDefault();
        }
    }
}
