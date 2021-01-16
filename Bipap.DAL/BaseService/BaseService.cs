using Bipap.DAL.BaseRepository;
using Bipap.DAL.Models;
using Bipap.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bipap.DAL.BaseService
{
    public abstract class BaseService<T> : IBaseService<T> where T : BaseModel
    {
        IUnitOfWork _unitOfWork;
        IBaseRepository<T> _repository;
        public BaseService(IUnitOfWork unitOfWork, IBaseRepository<T> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public ResultMessage Create(T model)
        {
            try
            {
                _repository.Add(model);
                var id = _unitOfWork.Commit();
                return new ResultMessage { IsSubmited = true, Message = "Operation is Completed", Id = model.Id };
            }
            catch (Exception ex)
            {
                return new ResultMessage { IsSubmited = false, Message = ex.Message };
            }
        }

        public async Task<ResultMessage> CreateAsync(T model)
        {
            try
            {
                await _repository.AddAsync(model);
                await _unitOfWork.CommitAsync();
                return await Task.Run(() => new ResultMessage { IsSubmited = true, Message = "Operation is Completed" });
            }
            catch (Exception ex)
            {
                return await Task.Run(() => new ResultMessage { IsSubmited = false, Message = ex.Message });
            }
        }

        public ResultMessage Delete(T model)
        {
            try
            {
                _repository.Remove(model);
                _unitOfWork.Commit();
                return new ResultMessage { IsSubmited = true, Message = "Operation is Completed" };
            }
            catch (Exception ex)
            {
                return new ResultMessage { IsSubmited = false, Message = ex.Message };
            }
        }

        public async Task<ResultMessage> DeleteAsync(T model)
        {
            try
            {
                _repository.Remove(model);
                await _unitOfWork.CommitAsync();
                return await Task.Run(() => new ResultMessage { IsSubmited = true, Message = "Operation is Completed" });
            }
            catch (Exception ex)
            {
                return await Task.Run(() => new ResultMessage { IsSubmited = false, Message = ex.Message });
            }
        }

        public T Get(int id)
        {
            return _repository.GetById(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _repository.GetAll();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<T> GetAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public ResultMessage Update(T model)
        {
            try
            {
                _repository.Edit(model);
                _unitOfWork.Commit();
                return new ResultMessage { IsSubmited = true, Message = "Operation is Completed" };
            }
            catch (Exception ex)
            {
                return new ResultMessage { IsSubmited = false, Message = ex.Message };
            }
        }

        public async Task<ResultMessage> UpdateAsync(T model)
        {
            try
            {
                _repository.Edit(model);
                await _unitOfWork.CommitAsync();
                return await Task.Run(() => new ResultMessage { IsSubmited = true, Message = "Operation is Completed" });
            }
            catch (Exception ex)
            {
                return await Task.Run(() => new ResultMessage { IsSubmited = false, Message = ex.Message });
            }
        }
    }
}
