using Bipap.DAL.BaseService;
using Bipap.DAL.Models;
using Bipap.DAL.UnitOfWork;
using Bipap.Repository.IReposirories;
using Bipap.Service.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bipap.Service.Services
{
    public class StepOneModuleService : BaseService<StepOneModule>, IStepOneModuleService
    {
        IUnitOfWork _unitOfWork;
        IStepOneModuleRepository _stepOneModuleRepository;

        public StepOneModuleService(IUnitOfWork unitOfWork, IStepOneModuleRepository stepOneModuleRepository)
            : base(unitOfWork, stepOneModuleRepository)
        {
            _unitOfWork = unitOfWork;
            _stepOneModuleRepository = stepOneModuleRepository;
        }

        public IEnumerable<StepOneModule> GetByFileId(int fileId)
        {
            return _stepOneModuleRepository.GetByFileId(fileId);
        }

        public StepOneModule GetStepOneModuleWithFile(int stepOneModuleId)
        {
            return _stepOneModuleRepository.GetStepOneModuleWithFile(stepOneModuleId);
        }
    }
}
