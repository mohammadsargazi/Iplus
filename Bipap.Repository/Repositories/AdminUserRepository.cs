using Bipap.DAL;
using Bipap.DAL.BaseRepository;
using Bipap.DAL.Models;
using Bipap.Repository.IReposirories;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Bipap.Repository.Repositories
{
    public class AdminUserRepository : BaseRepository<AdminUser>, IAdminUserRepository
    {
        private BipapDbContext BipapDbContext
        {
            get { return Context as BipapDbContext; }
        }
        public AdminUserRepository(DbContext context)
              : base(context) { }

        public AdminUser Valid(string userName, string password)
        {
            return BipapDbContext.AdminUsers
                .Where(x => x.UserName == userName && x.Password == password)
                .FirstOrDefault();
        }
    }
}
