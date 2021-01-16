using Bipap.DAL.BaseService;
using Bipap.DAL.Models;
using System.Threading.Tasks;

namespace Bipap.Service.IServices
{
    public interface ISupportUserService : IBaseService<SupportUser>
    {
        SupportUser GetByMobile(string mobile);
        SupportUser CheckActiveCode(string mobile, string activeCode);
        bool IsValidActiveCode(int userId, string activeCode);
    }
}
