using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using RepositoryLayer.Context;
using RepositoryLayer.IRepo;
using RepositoryLayer.Repo;
using RepositoryLayer.UnitOfWork;
using ServiceLayer.IServices;
using ServiceLayer.Mapping;
using ServiceLayer.Services;
using SharedDTO;
using System;
using System.Linq;
using System.Reflection;

namespace Luftborn
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Enable Cors
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());               
            });
            #endregion

            #region Context
            services.AddDbContext<ApplicationDBContext>(option => option.UseSqlServer(Configuration.GetConnectionString("LuftbornCon"),
                sqlServerOptionsAction : sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                    sqlOptions.EnableRetryOnFailure(maxRetryCount: 15,
                        maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                }
                ).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
            #endregion

            #region Json Serializer
            services.AddControllersWithViews().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            #endregion

            //addSwaggerDocument
            services.AddSwaggerDocument();

            services.AddControllers();

            

            #region AutoMapper
            services.AddAutoMapper(typeof(UserMapping).Assembly);
            services.AddAutoMapper(typeof(ProductMapping).Assembly);
            #endregion

            #region Repo AddScoped
            Type[] repositories = Assembly.Load(typeof(UserRepo).Assembly.GetName()).GetTypes().Where(r => r.IsClass && r.Name.EndsWith("Repo") ).ToArray();
            Type[] iRepositories = Assembly.Load(typeof(IUserRepo).Assembly.GetName()).GetTypes().Where(r =>r.IsInterface && r.Name.EndsWith("Repo")).ToArray();
            foreach (var repoInterface in iRepositories)
            {
                System.Type classType = repositories.FirstOrDefault(r => repoInterface.IsAssignableFrom(r));
                if(classType != null)
                {
                    services.AddScoped(repoInterface, classType);
                }
            }

            #endregion

            #region Services AddScoped
            Type[] appservices = Assembly.Load(typeof(UserService).Assembly.GetName()).GetTypes().Where(r => r.IsClass && r.Name.EndsWith("Service")).ToArray();
            Type[] iappservices = Assembly.Load(typeof(IUserService).Assembly.GetName()).GetTypes().Where(r => r.IsInterface && r.Name.EndsWith("Service")).ToArray();
            foreach (var repoInterface in iappservices)
            {
                System.Type classType = appservices.FirstOrDefault(r => repoInterface.IsAssignableFrom(r));
                if (classType != null)
                {
                    services.AddScoped(repoInterface, classType);
                }
            }
            #endregion

            #region UnitOfWord
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
                app.UseOpenApi();
                app.UseSwaggerUi3();
            }

            //Enable Cors
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
