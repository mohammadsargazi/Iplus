using Bipap.DAL.BaseRepository;
using Bipap.DAL.Models;

namespace Bipap.Repository.IReposirories
{
    public interface IAdminUserRepository : IBaseRepository<AdminUser>
    {
        AdminUser Valid(string userName, string password);
    }
}
