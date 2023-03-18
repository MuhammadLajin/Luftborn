using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer
{
    public interface IInsertRepository<T> where T : class
    {
        #region Insert
        T Insert(T entity);

        #endregion
    }
}
