using Bipap.DAL.BaseService;
using Bipap.DAL.Models;
using Bipap.DAL.UnitOfWork;
using Bipap.Repository.IReposirories;
using Bipap.Service.IServices;
using System.Collections.Generic;

namespace Bipap.Service.Services
{
    public class SupportUserOrderService : BaseService<SupportUserOrder>, ISupportUserOrderService
    {
        IUnitOfWork _unitOfWork;
        ISupportUserOrderRepository _supportUserOrderRepository;

        public SupportUserOrderService(IUnitOfWork unitOfWork, ISupportUserOrderRepository supportUserOrderRepository)
            : base(unitOfWork, supportUserOrderRepository)
        {
            _unitOfWork = unitOfWork;
            _supportUserOrderRepository = supportUserOrderRepository;
        }

        public IEnumerable<SupportUserOrder> GetBySupportUserId(int supportUserId)
        {
            return _supportUserOrderRepository.GetBySupportUserId(supportUserId);
        }
    }
}
