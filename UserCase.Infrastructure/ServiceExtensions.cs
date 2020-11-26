using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using UserCase.Core.Entities;
using UserCase.Infrastructure.Repositories;
using UserCase.Infrastructure.Services;

namespace UserCase.Infrastructure
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddIdentity<User, UserRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<DbContext>().AddDefaultTokenProviders();
            services.AddDbContext<DbContext, UserCaseDbContext>(options => options.UseNpgsql(connectionString));
            return services;
        }

        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    // options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = configuration["JWT:ValidAudience"],
                        ValidIssuer = configuration["JWT:ValidIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
                    };
                });
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            var serviceMainType = typeof(IService);
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).ToArray();
            var interfaces = types.Where(x => serviceMainType.IsAssignableFrom(x) && x.IsInterface);
            foreach (var interfaceType in interfaces)
            {
                var foundType = types.SingleOrDefault(x =>
                    interfaceType.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract);
                if (foundType != null)
                {
                    services.AddScoped(interfaceType, foundType);
                }
            }

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            var baseType = typeof(IBaseEntity);
            var entityTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).Where(x =>
                baseType.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract);
            var repositoryInterfaceType = typeof(IRepository<>);
            var repositoryBaseType = typeof(BaseRepository<>);
            foreach (var entityType in entityTypes)
            {
                var genericInterfaceType = repositoryInterfaceType.MakeGenericType(entityType);
                var genericBaseType = repositoryBaseType.MakeGenericType(entityType);
                services.AddScoped(genericInterfaceType, genericBaseType);
            }

            return services;
        }
    }
}