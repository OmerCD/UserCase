using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserCase.Contract.User;
using UserCase.Core.Entities;
using UserCase.Infrastructure.Services.Interfaces;

namespace UserCase.Domain.User.Commands
{
    public class UserRegisterCommand : IRequest<UserRegisterResponseViewModel>
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Email { get; init; }
        public string UserName { get; init; }
        public string Password { get; init; }
        public string AddressStreet { get; init; }
        public string AddressSuite { get; init; }
        public string AddressCity { get; init; }
        public string AddressZipCode { get; init; }
        public double GeoLatitude { get; init; }
        public double GeoLongitude { get; init; }
        public string Phone { get; init; }
        public string Website { get; init; }
        public int Company { get; init; }

        public UserRegisterCommand(string firstName, string lastName, string email, string userName, string password,
            string addressStreet, string addressSuite, string addressCity, string addressZipCode, in double geoLatitude,
            in double geoLongitude, string phone, string website, in int company)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            UserName = userName;
            Password = password;
            AddressStreet = addressStreet;
            AddressSuite = addressSuite;
            AddressCity = addressCity;
            AddressZipCode = addressZipCode;
            GeoLatitude = geoLatitude;
            GeoLongitude = geoLongitude;
            Phone = phone;
            Website = website;
            Company = company;
        }
        public UserRegisterCommand()
        {

        }
    }

    public class UserRegisterCommandHandler : IRequestHandler<UserRegisterCommand, UserRegisterResponseViewModel>
    {
        private readonly IUserService _userService;

        public UserRegisterCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<UserRegisterResponseViewModel> Handle(UserRegisterCommand request,
            CancellationToken cancellationToken)
        {
            var user = new Core.Entities.User
            {
                Name = request.FirstName + ' ' + request.LastName,
                UserName = request.UserName,
                Email = request.Email,
                Address = new Address()
                {
                    City = request.AddressCity,
                    Street = request.AddressStreet,
                    Suite = request.AddressSuite,
                    ZipCode = request.AddressZipCode,
                    Geo = new Location()
                    {
                        Latitude = request.GeoLatitude,
                        Longitude = request.GeoLongitude
                    }
                },
                Phone = request.Phone,
                Website = request.Website,
                CompanyId = request.Company
            };
            var identityResult = await _userService.RegisterUser(user, request.Password);
            if (!identityResult.Succeeded)
            {
                return new UserRegisterResponseViewModel()
                {
                    Errors = identityResult.Errors.Select(x => x.Description).ToArray()
                };
            }

            return new UserRegisterResponseViewModel()
            {
                FullName = user.Name
            };
        }
    }
}