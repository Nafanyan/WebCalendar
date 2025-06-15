using Application.Interfaces;
using Application.Result;
using Application.Users.DTOs;
using Application.Validation;
using Domain.Entities;
using Application.Repositories;

namespace Application.Users.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQueryDto, GetUserByIdQuery>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAsyncValidator<GetUserByIdQuery> _getUserByIdQueryValidator;

        public GetUserByIdQueryHandler( IUserRepository userRepository, IAsyncValidator<GetUserByIdQuery> validator )
        {
            _userRepository = userRepository;
            _getUserByIdQueryValidator = validator;
        }

        public async Task<QueryResult<GetUserByIdQueryDto>> HandleAsync( GetUserByIdQuery getUserByIdQuery )
        {
            ValidationResult validationResult = await _getUserByIdQueryValidator.ValidationAsync( getUserByIdQuery );
            if( validationResult.IsFail )
            {
                return new QueryResult<GetUserByIdQueryDto>( validationResult );
            }

            User user = await _userRepository.GetByIdAsync( getUserByIdQuery.Id );
            GetUserByIdQueryDto getUserByIdQueryDto = new GetUserByIdQueryDto
            {
                Id = user.Id,
                Login = user.Login
            };
            return new QueryResult<GetUserByIdQueryDto>( getUserByIdQueryDto );
        }
    }
}
