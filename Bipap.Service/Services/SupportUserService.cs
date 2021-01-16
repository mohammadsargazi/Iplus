using Bipap.DAL.BaseService;
using Bipap.DAL.Models;
using Bipap.DAL.UnitOfWork;
using Bipap.Repository.IReposirories;
using Bipap.Service.IServices;
using System.Threading.Tasks;

namespace Bipap.Service.Services
{
    public class SupportUserService : BaseService<SupportUser>, ISupportUserService
    {
        IUnitOfWork _unitOfWork;
        ISupportUserRepository _supportUserRepository;

        public SupportUserService(IUnitOfWork unitOfWork, ISupportUserRepository supportUserRepository)
            : base(unitOfWork, supportUserRepository)
        {
            _unitOfWork = unitOfWork;
            _supportUserRepository = supportUserRepository;
        }

        public SupportUser CheckActiveCode(string mobile, string activeCode)
        {
            return _supportUserRepository.CheckActiveCode(mobile, activeCode);
        }

        public SupportUser GetByMobile(string mobile)
        {
            return _supportUserRepository.GetByMobile(mobile);
        }

        public bool IsValidActiveCode(int userId, string activeCode)
        {
            return _supportUserRepository.IsValidActiveCode(userId, activeCode);
        }
    }
}
