using Bipap.DAL.BaseService;
using Bipap.DAL.Models;
using Bipap.DAL.UnitOfWork;
using Bipap.Repository.IReposirories;
using Bipap.Service.IServices;

namespace Bipap.Service.Services
{
   public class SettelmentStatusService : BaseService<SettelmentStatus>, ISettelmentStatusService
    {
        IUnitOfWork _unitOfWork;
        ISettelmentStatusRepository _settelmentStatusRepository;

        public SettelmentStatusService(IUnitOfWork unitOfWork, ISettelmentStatusRepository settelmentStatusRepository)
            : base(unitOfWork, settelmentStatusRepository)
        {
            _unitOfWork = unitOfWork;
            _settelmentStatusRepository = settelmentStatusRepository;
        }
    }
}
