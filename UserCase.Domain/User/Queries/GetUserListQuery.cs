using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserCase.Contract.User;

namespace UserCase.Domain.User.Queries
{
    public class GetUserListQuery : IRequest<IEnumerable<UserViewModel>>
    {
        
    }
    public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery,IEnumerable<UserViewModel>>
    {
        private readonly UserManager<Core.Entities.User> _userManager;
        private readonly IMapper _mapper;

        public GetUserListQueryHandler(UserManager<Core.Entities.User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public Task<IEnumerable<UserViewModel>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            var users = _userManager.Users.AsNoTracking();
            var mapped = _mapper.Map<IEnumerable<UserViewModel>>(users);
            return Task.FromResult(mapped);
        }
    }
}