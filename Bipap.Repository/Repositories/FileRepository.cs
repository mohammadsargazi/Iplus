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
    public class FileRepository : BaseRepository<File>, IFileRepository
    {
        private BipapDbContext BipapDbContext
        {
            get { return Context as BipapDbContext; }
        }
        public FileRepository(DbContext context)
              : base(context) { }

        public IEnumerable<File> GetByPatientId(int pateientId)
        {
            return BipapDbContext.Files
                .Where(x => x.PatientId == pateientId)
                .Include(x => x.FileUploadType)
                .ToList();
        }

        public File GetFileWithPatientById(int id)
        {
            return BipapDbContext.Files
                .Where(x => x.Id == id)
                .Include(x => x.Patient)
                .FirstOrDefault();
        }
    }
}
