using Bipap.DAL;
using Bipap.DAL.BaseRepository;
using Bipap.DAL.Models;
using Bipap.Repository.IReposirories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;


namespace Bipap.Repository.Repositories
{
   public class SupportUserOrderRepository : BaseRepository<SupportUserOrder>, ISupportUserOrderRepository
    {
        private BipapDbContext BipapDbContext
        {
            get { return Context as BipapDbContext; }
        }
        public SupportUserOrderRepository(DbContext context)
              : base(context) { }

        public IEnumerable<SupportUserOrder> GetBySupportUserId(int supportUserId)
        {
            return BipapDbContext.SupportUserOrders
                .Where(x => x.SupportUserId == supportUserId)
                .Include(x => x.SettelmentStatus)
                .ToList();
        }
    }
}
