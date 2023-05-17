using Application.Interfaces;
using Application.Result;
using Application.Validation;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Users.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : IQueryHandler<User, GetUserByIdQuery>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAsyncValidator<GetUserByIdQuery> _getUserByIdQueryValidator;

        public GetUserByIdQueryHandler(IUserRepository userRepository, IAsyncValidator<GetUserByIdQuery> validator)
        {
            _userRepository = userRepository;
            _getUserByIdQueryValidator = validator;
        }

        public async Task<QueryResult<User>> Handle(GetUserByIdQuery getUserByIdQuery)
        {
            ValidationResult validationResult = await _getUserByIdQueryValidator.Validation(getUserByIdQuery);
            if (!validationResult.IsFail)
            {
                User user = await _userRepository.GetById(getUserByIdQuery.Id);
                return new QueryResult<User>(validationResult, user);
            }
            return new QueryResult<User>(validationResult);
        }
    }
}
