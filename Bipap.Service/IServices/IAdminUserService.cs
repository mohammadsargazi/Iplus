using Bipap.DAL.BaseService;
using Bipap.DAL.Models;

namespace Bipap.Service.IServices
{
    public interface IAdminUserService : IBaseService<AdminUser>
    {
        AdminUser Valid(string userName, string password);
    }
}
