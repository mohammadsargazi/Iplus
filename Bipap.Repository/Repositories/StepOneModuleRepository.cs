using Bipap.DAL;
using Bipap.DAL.BaseRepository;
using Bipap.DAL.Models;
using Bipap.Repository.IReposirories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bipap.Repository.Repositories
{
    public class StepOneModuleRepository : BaseRepository<StepOneModule>, IStepOneModuleRepository
    {
        private BipapDbContext BipapDbContext
        {
            get { return Context as BipapDbContext; }
        }
        public StepOneModuleRepository(DbContext context)
              : base(context) { }

        public IEnumerable<StepOneModule> GetByFileId(int fileId)
        {
            return BipapDbContext.StepOneModules
                .Where(x => x.FileId == fileId).ToList();
        }

        public StepOneModule GetStepOneModuleWithFile(int stepOneModuleId)
        {
            return BipapDbContext.StepOneModules
                .Where(x => x.Id == stepOneModuleId)
                .Include(x => x.File)
                .FirstOrDefault();
        }
    }
}
