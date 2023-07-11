using Application.Interfaces;
using Application.Result;
using Application.Users.DTOs;
using Application.Validation;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Users.Queries.QueryUserById
{
    public class UserQueryHandlerById : IQueryHandler<UserQueryByIdDto, UserQueryById>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAsyncValidator<UserQueryById> _userByIdQueryValidator;

        public UserQueryHandlerById(IUserRepository userRepository, IAsyncValidator<UserQueryById> validator)
        {
            _userRepository = userRepository;
            _userByIdQueryValidator = validator;
        }

        public async Task<QueryResult<UserQueryByIdDto>> HandleAsync(UserQueryById getUserByIdQuery)
        {
            ValidationResult validationResult = await _userByIdQueryValidator.ValidationAsync(getUserByIdQuery);
            if (!validationResult.IsFail)
            {
                User user = await _userRepository.GetByIdAsync(getUserByIdQuery.Id);
                UserQueryByIdDto getUserByIdQueryDto = new UserQueryByIdDto
                {
                    Id = user.Id,
                    Login = user.Login
                };
                return new QueryResult<UserQueryByIdDto>(getUserByIdQueryDto);
            }
            return new QueryResult<UserQueryByIdDto>(validationResult);
        }
    }
}
