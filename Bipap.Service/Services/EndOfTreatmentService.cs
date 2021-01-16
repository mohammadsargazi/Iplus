using Bipap.DAL.BaseService;
using Bipap.DAL.Models;
using Bipap.DAL.UnitOfWork;
using Bipap.Repository.IReposirories;
using Bipap.Service.IServices;
using System.Threading.Tasks;

namespace Bipap.Service.Services
{
    public class EndOfTreatmentService : BaseService<EndOfTreatment>, IEndOfTreatmentService
    {
        IUnitOfWork _unitOfWork;
        IEndOfTreatmentRepository _endOfTreatmentRepository;

        public EndOfTreatmentService(IUnitOfWork unitOfWork, IEndOfTreatmentRepository endOfTreatmentRepository)
            : base(unitOfWork, endOfTreatmentRepository)
        {
            _unitOfWork = unitOfWork;
            _endOfTreatmentRepository = endOfTreatmentRepository;
        }

        public EndOfTreatment GetByDeviceIdWithStatus(int deviceId)
        {
            return _endOfTreatmentRepository.GetByDeviceIdWithStatus(deviceId);
        }
    }
}
