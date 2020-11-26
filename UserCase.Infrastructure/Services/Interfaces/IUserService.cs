using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using UserCase.Core.Entities;

namespace UserCase.Infrastructure.Services.Interfaces
{
    public interface IUserService : IService
    {
        Task<IdentityResult> RegisterUser(User user, string password);
    }
}
