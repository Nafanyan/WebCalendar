using Application.Result;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Users.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : BaseUserUseCase, IUserQueryHandler<User, GetUserByIdQuery>
    {
        private readonly GetUserByIdQueryValidation _getUserByIdQueryValidation;

        public GetUserByIdQueryHandler(IUserRepository userRepository) : base(userRepository)
        {
            _getUserByIdQueryValidation = new GetUserByIdQueryValidation(userRepository);
        }

        public async Task<ResultQuery<User>> Handle(GetUserByIdQuery getUserByIdQuery)
        {
            string msg = _getUserByIdQueryValidation.Validation(getUserByIdQuery);
            if (msg == "Ok")
            {
                return new ResultQuery<User>(await _userRepository.GetById(getUserByIdQuery.Id), msg);

            }
            return new ResultQuery<User>(msg);
        }
    }
}
