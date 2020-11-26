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
    public class GetUserInfoQuery : IRequest<UserViewModel>
    {
        public int UserId { get; init; }

        public GetUserInfoQuery(int userId)
        {
            UserId = userId;
        }
    }
    public class GetUserInfoQueryHandler: IRequestHandler<GetUserInfoQuery, UserViewModel>
    {
        private readonly UserManager<Core.Entities.User> _userManager;
        private readonly IMapper _mapper;

        public GetUserInfoQueryHandler(UserManager<Core.Entities.User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<UserViewModel> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
        {
            var activeUser = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken: cancellationToken);
            return _mapper.Map<UserViewModel>(activeUser);
        }
    }
}