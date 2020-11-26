using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Reflection;
using UserCase.Core.Entities;
using UserCase.Core.Entities.Configurations;

namespace UserCase.Infrastructure
{
    public class UserCaseDbContext : IdentityDbContext<User, UserRole, int>
    {
        public UserCaseDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
        //public DbSet<User> Users { get; set; }

    }
}
