using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using UserCase.Contract.User;

namespace UserCase.Domain.User.Commands
{
    public class UserLoginCommand : IRequest<UserLoginResponseViewModel>
    {
        public string UserName { get; init; }
        public string Password { get; init; }

        public UserLoginCommand(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }

    public class UserLoginCommandHandler : IRequestHandler<UserLoginCommand, UserLoginResponseViewModel>
    {
        private readonly UserManager<Core.Entities.User> _userManager;
        private readonly IConfiguration _configuration;

        public UserLoginCommandHandler(UserManager<Core.Entities.User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<UserLoginResponseViewModel> Handle(UserLoginCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
                return null;
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: claims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new UserLoginResponseViewModel()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo
            };
        }
    }
}