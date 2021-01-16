using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bipap.DAL.UnitOfWork
{
    public interface IUnitOfWork: IDisposable
    {
        Task<int> CommitAsync();
        int Commit();
    }
}
