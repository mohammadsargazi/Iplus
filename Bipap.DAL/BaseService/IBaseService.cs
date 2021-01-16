using Bipap.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bipap.DAL.BaseService
{
    public interface IBaseService<T>
     where T : BaseModel
    {
        Task<T> GetAsync(int id);
        T Get(int id);
        Task<IEnumerable<T>> GetAllAsync();
        IEnumerable<T> GetAll();
        Task<ResultMessage> CreateAsync(T model);
        ResultMessage Create(T model);
        Task<ResultMessage> UpdateAsync(T model);
        ResultMessage Update(T model);
        Task<ResultMessage> DeleteAsync(T model);
        ResultMessage Delete(T model);
    }
}
