using Bipap.DAL.BaseRepository;
using Bipap.DAL.Models;
using System.Collections.Generic;

namespace Bipap.Repository.IReposirories
{
    public interface ISupportUserOrderRepository : IBaseRepository<SupportUserOrder>
    {
        IEnumerable<SupportUserOrder> GetBySupportUserId(int supportUserId);
    }
}
