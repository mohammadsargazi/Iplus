using Bipap.DAL.BaseService;
using Bipap.DAL.Models;
using System.Collections.Generic;

namespace Bipap.Service.IServices
{
    public interface ISupportUserOrderService : IBaseService<SupportUserOrder>
    {
        IEnumerable<SupportUserOrder> GetBySupportUserId(int supportUserId);
    }
}
