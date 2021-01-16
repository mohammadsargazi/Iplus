using Bipap.DAL.BaseService;
using Bipap.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bipap.Service.IServices
{
    public interface IStepOneModuleService : IBaseService<StepOneModule>
    {
        IEnumerable<StepOneModule> GetByFileId(int fileId);
        StepOneModule GetStepOneModuleWithFile(int stepOneModuleId);
    }
}
