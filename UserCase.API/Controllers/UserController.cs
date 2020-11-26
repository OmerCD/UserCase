using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using UserCase.Contract.User;
using UserCase.Domain.User.Commands;
using UserCase.Domain.User.Queries;
using UserCase.Extensions;

namespace UserCase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterModel model)
        {
            var command = new UserRegisterCommand
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.UserName,
                Password = model.Password,
                AddressStreet = model.Address.Street,
                AddressSuite = model.Address.Suite,
                AddressCity = model.Address.City,
                AddressZipCode = model.Address.ZipCode,
                GeoLatitude = model.Address.Geo.Lat,
                GeoLongitude = model.Address.Geo.Lng,
                Phone = model.Phone,
                Company = model.Company
            };
            return new ObjectResult(await _mediator.Send(command))
            {
                StatusCode = 201
            };
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginModel model)
        {
            var command = new UserLoginCommand(model.UserName, model.Password);
            var response = await _mediator.Send(command);
            if (response == null)
            {
                return Unauthorized();
            }

            return Ok(response);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateUserInfo(UserUpdateModel model)
        {
            var command = new UserUpdateCommand
            {
                Id = User.GetId(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                AddressStreet = model.Address.Street,
                AddressSuite = model.Address.Suite,
                AddressCity = model.Address.City,
                AddressZipCode = model.Address.ZipCode,
                GeoLatitude = model.Address.Geo.Lat,
                GeoLongitude = model.Address.Geo.Lng,
                Phone = model.Phone,
                Company = model.Company
            };
            
            throw new NotImplementedException();
        }

        [HttpGet("list")]
        [Authorize]
        public async Task<IActionResult> GetUserList()
        {
            var query = new GetUserListQuery();
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserInfo()
        {
            var query = new GetUserInfoQuery(User.GetId());

            var response = await _mediator.Send(query);
            return Ok(response);
        }
    }
}