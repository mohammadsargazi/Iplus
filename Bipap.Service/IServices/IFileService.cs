using Bipap.DAL.BaseService;
using Bipap.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bipap.Service.IServices
{
    public interface IFileService : IBaseService<File>
    {
        IEnumerable<File> GetByPatientId(int pateientId);
        File GetFileWithPatientById(int id);

    }
}
