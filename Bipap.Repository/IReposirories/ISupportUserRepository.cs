using Bipap.DAL.BaseRepository;
using Bipap.DAL.Models;
using System.Threading.Tasks;

namespace Bipap.Repository.IReposirories
{
    public interface ISupportUserRepository : IBaseRepository<SupportUser>
    {
        SupportUser GetByMobile(string mobile);
        SupportUser CheckActiveCode(string mobile, string activeCode);
        bool IsValidActiveCode(int userId, string activeCode);
    }
}
