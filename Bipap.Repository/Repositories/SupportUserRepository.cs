using Bipap.DAL;
using Bipap.DAL.BaseRepository;
using Bipap.DAL.Models;
using Bipap.Repository.IReposirories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
namespace Bipap.Repository.Repositories
{
    public class SupportUserRepository : BaseRepository<SupportUser>, ISupportUserRepository
    {
        private BipapDbContext BipapDbContext
        {
            get { return Context as BipapDbContext; }
        }
        public SupportUserRepository(DbContext context)
              : base(context) { }

        public SupportUser GetByMobile(string mobile)
        {
            return BipapDbContext.SupportUsers
                .Where(x => x.Mobile == mobile)
                .FirstOrDefault();
        }

        public SupportUser CheckActiveCode(string mobile, string activeCode)
        {
            return BipapDbContext.SupportUsers
               .Where(x => x.Mobile == mobile && x.ActiveCode == activeCode).FirstOrDefault();
        }

        public bool IsValidActiveCode(int userId, string activeCode)
        {
            return BipapDbContext.SupportUsers
                .Any(x => x.Id == userId && x.ActiveCode == activeCode);
        }
    }
}
