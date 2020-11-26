using MediatR;
using UserCase.Contract.User;

namespace UserCase.Domain.User.Commands
{
    public class UserUpdateCommand : IRequest<UserUpdateResponseViewModel>
    {
        public int Id { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Email { get; init; }
        public string AddressSuite { get; init; }
        public string AddressCity { get; init; }
        public string AddressZipCode { get; init; }
        public string AddressStreet { get; init; }
        public double GeoLatitude { get; init; }
        public double GeoLongitude { get; init; }
        public string Phone { get; init; }
        public int Company { get; init; }
    }
}