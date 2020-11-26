using AutoMapper;
using UserCase.Contract.User;
using UserCase.Core.Entities;

namespace UserCase.Domain.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<Core.Entities.User, UserViewModel>()
                .ForMember(x=>x.FullName, expression => expression.MapFrom(user => user.Name));
            CreateMap<Address, UserAddressViewModel>();
            CreateMap<Location, UserAddressLocationViewModel>();
        }
    }
}