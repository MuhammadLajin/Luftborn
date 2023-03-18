using RepositoryLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool disposedValue;
        private readonly ApplicationDBContext Context;
        public UnitOfWork(ApplicationDBContext context)
        {
            Context = context;
        }
        

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                if (disposing)
                {
                    //dispose manages state
                }
            }
            disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                var changes = await Context.SaveChangesAsync();
                return changes > default(byte);
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
