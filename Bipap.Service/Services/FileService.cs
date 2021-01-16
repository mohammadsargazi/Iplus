using Bipap.DAL.BaseService;
using Bipap.DAL.Models;
using Bipap.DAL.UnitOfWork;
using Bipap.Repository.IReposirories;
using Bipap.Service.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bipap.Service.Services
{
    public class FileService : BaseService<File>, IFileService
    {
        IUnitOfWork _unitOfWork;
        IFileRepository _fileRepository;

        public FileService(IUnitOfWork unitOfWork, IFileRepository fileRepository)
            : base(unitOfWork, fileRepository)
        {
            _unitOfWork = unitOfWork;
            _fileRepository = fileRepository;
        }

        public IEnumerable<File> GetByPatientId(int pateientId)
        {
            return _fileRepository.GetByPatientId(pateientId);
        }

        public File GetFileWithPatientById(int id)
        {
            return _fileRepository.GetFileWithPatientById(id);
        }
    }
}
