using Bipap.DAL.BaseRepository;
using Bipap.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bipap.Repository.IReposirories
{
    public interface IStepOneModuleRepository : IBaseRepository<StepOneModule>
    {
        IEnumerable<StepOneModule> GetByFileId(int fileId);
        StepOneModule GetStepOneModuleWithFile(int stepOneModuleId);
    }
}
