using DomainLayer.Configuration;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace RepositoryLayer.Context
{
    public class ApplicationDBContext : DbContext
    {
        #region props
        public DbSet<User> User { get; set; }
       
        #endregion

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelbuider)
        {
            modelbuider.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
            //seeding the enums
            modelbuider.BuildEnums();
            base.OnModelCreating(modelbuider);
        }

    }
}
