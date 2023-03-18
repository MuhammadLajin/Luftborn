using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer
{
    public interface IRetrieveRepository<T> where T : class
    {
        #region Retrieve
        T Get(T entity);

        #endregion
    }
}
