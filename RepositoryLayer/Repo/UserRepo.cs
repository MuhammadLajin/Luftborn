using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.IRepo;

namespace RepositoryLayer.Repo
{
    public class UserRepo : Repository<User>, IUserRepo
    {
        #region props
        private readonly IConfiguration Configuration;
        private readonly ApplicationDBContext Context;
        private DbSet<User> entity;

        public UserRepo(ApplicationDBContext context, IConfiguration configuration) : base(context)
        {
            entity = context.Set<User>();
            Context = context;
            Configuration = configuration;
        }

        #endregion
    }
}
