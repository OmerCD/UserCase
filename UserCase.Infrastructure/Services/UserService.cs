using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using UserCase.Core.Entities;
using UserCase.Infrastructure.Repositories;
using UserCase.Infrastructure.Services.Interfaces;

namespace UserCase.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> RegisterUser(User user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return result;
        }
    }
}
