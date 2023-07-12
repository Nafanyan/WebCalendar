using Application.Interfaces;
using Application.Result;
using Application.Users.DTOs;
using Application.Validation;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Users.Queries.QueryUserById
{
    public class UserQueryHandlerById : IQueryHandler<GetUserByIdQueryDto, GetUserByIdQuery>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAsyncValidator<GetUserByIdQuery> _userByIdQueryValidator;

        public UserQueryHandlerById(IUserRepository userRepository, IAsyncValidator<GetUserByIdQuery> validator)
        {
            _userRepository = userRepository;
            _userByIdQueryValidator = validator;
        }

        public async Task<QueryResult<GetUserByIdQueryDto>> HandleAsync(GetUserByIdQuery getUserByIdQuery)
        {
            ValidationResult validationResult = await _userByIdQueryValidator.ValidationAsync(getUserByIdQuery);
            if (validationResult.IsFail)
            {
                return new QueryResult<GetUserByIdQueryDto>(validationResult);
            }

            User user = await _userRepository.GetByIdAsync(getUserByIdQuery.Id);
            GetUserByIdQueryDto getUserByIdQueryDto = new GetUserByIdQueryDto
            {
                Id = user.Id,
                Login = user.Login
            };
            return new QueryResult<GetUserByIdQueryDto>(getUserByIdQueryDto);
        }
    }
}
