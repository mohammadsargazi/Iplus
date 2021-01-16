using Bipap.DAL;
using Bipap.DAL.BaseRepository;
using Bipap.DAL.Models;
using Bipap.Repository.IReposirories;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Bipap.Repository.Repositories
{
    public class SettelmentStatusRepository : BaseRepository<SettelmentStatus>, ISettelmentStatusRepository
    {
        private BipapDbContext BipapDbContext
        {
            get { return Context as BipapDbContext; }
        }
        public SettelmentStatusRepository(DbContext context)
              : base(context) { }
    }
}
