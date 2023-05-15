using Application.Result;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Users.Queries.GetUsers
{
    public class GetUsersQueryHandler : BaseUserUseCase, IUserQueryHandler<IReadOnlyList<User>, GetUsersQuery>
    {
        public GetUsersQueryHandler(IUserRepository userRepository) : base(userRepository)
        {
        }

        public async Task<ResultQuery<IReadOnlyList<User>>> Handle(GetUsersQuery query)
        {
            return new ResultQuery<IReadOnlyList<User>>(await _userRepository.GetAll(), "Ok");
        }
    }
}
