using Application.Interfaces;
using Application.Result;
using Application.Users.DTOs;
using Application.Validation;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Users.Queries.QueryUserById
{
    public class UserQueryHandlerById : IQueryHandler<DTOs.UserQueryById, UserQueryById>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAsyncValidator<UserQueryById> _getUserByIdQueryValidator;

        public UserQueryHandlerById(IUserRepository userRepository, IAsyncValidator<UserQueryById> validator)
        {
            _userRepository = userRepository;
            _getUserByIdQueryValidator = validator;
        }

        public async Task<QueryResult<DTOs.UserQueryById>> HandleAsync(UserQueryById getUserByIdQuery)
        {
            ValidationResult validationResult = await _getUserByIdQueryValidator.ValidationAsync(getUserByIdQuery);
            if (!validationResult.IsFail)
            {
                User user = await _userRepository.GetByIdAsync(getUserByIdQuery.Id);
                DTOs.UserQueryById getUserByIdQueryDto = new DTOs.UserQueryById
                {
                    Id = user.Id,
                    Login = user.Login
                };
                return new QueryResult<DTOs.UserQueryById>(getUserByIdQueryDto);
            }
            return new QueryResult<DTOs.UserQueryById>(validationResult);
        }
    }
}
