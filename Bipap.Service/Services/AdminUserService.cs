using Bipap.DAL.BaseService;
using Bipap.DAL.Models;
using Bipap.DAL.UnitOfWork;
using Bipap.Repository.IReposirories;
using Bipap.Service.IServices;

namespace Bipap.Service.Services
{
    public class AdminUserService : BaseService<AdminUser>, IAdminUserService
    {
        IUnitOfWork _unitOfWork;
        IAdminUserRepository _adminUserRepository;

        public AdminUserService(IUnitOfWork unitOfWork, IAdminUserRepository adminUserRepository)
            : base(unitOfWork, adminUserRepository)
        {
            _unitOfWork = unitOfWork;
            _adminUserRepository = adminUserRepository;
        }

        public AdminUser Valid(string userName, string password)
        {
            return _adminUserRepository.Valid(userName, password);
        }
    }
}
