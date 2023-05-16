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
        private readonly IValidator<GetUserByIdQuery> _getUserByIdQueryValidation;

        public GetUserByIdQueryHandler(IUserRepository userRepository, IValidator<GetUserByIdQuery> validator)
        {
            _userRepository = userRepository;
            _getUserByIdQueryValidation = validator;
        }

        public async Task<QueryResult<User>> Handle(GetUserByIdQuery getUserByIdQuery)
        {
            ValidationResult validationResult = _getUserByIdQueryValidation.Validation(getUserByIdQuery);
            if (!validationResult.IsFail)
            {
                return new QueryResult<User>(validationResult, await _userRepository.GetById(getUserByIdQuery.Id));
            }
            return new QueryResult<User>(validationResult);
        }
    }
}
