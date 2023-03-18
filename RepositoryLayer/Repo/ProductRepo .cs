using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.IRepo;

namespace RepositoryLayer.Repo
{
    public class ProductRepo : Repository<Product>, IProductRepo
    {
        #region props
        private readonly IConfiguration Configuration;
        private readonly ApplicationDBContext Context;
        private DbSet<Product> entity;

        public ProductRepo(ApplicationDBContext context, IConfiguration configuration) : base(context)
        {
            entity = context.Set<Product>();
            Context = context;
            Configuration = configuration;
        }

        #endregion
    }
}
