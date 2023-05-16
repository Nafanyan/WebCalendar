using Application.Interfaces;
using Application.Result;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Users.Queries.GetUsers
{
    public class GetUsersQueryHandler : IQueryHandler<IReadOnlyList<User>, GetUsersQuery>
    {
        private readonly IUserRepository _userRepository;
        public GetUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ResultQuery<IReadOnlyList<User>>> Handle(GetUsersQuery query)
        {
            return new ResultQuery<IReadOnlyList<User>>(await _userRepository.GetAll(), "Ok");
        }
    }
}
