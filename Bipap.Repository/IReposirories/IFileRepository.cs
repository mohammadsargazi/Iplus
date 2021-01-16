using Bipap.DAL.BaseRepository;
using Bipap.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bipap.Repository.IReposirories
{
    public interface IFileRepository: IBaseRepository<File>
    {
        IEnumerable<File> GetByPatientId(int pateientId);
        File GetFileWithPatientById(int id);
    }
}
