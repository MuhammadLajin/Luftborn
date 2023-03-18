using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.UnitOfWork
{
    public interface IUnitOfWork: IDisposable
    {
        Task<bool> SaveChangesAsync();
    }
}
