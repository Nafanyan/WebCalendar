using Application.Validation;
using Application.Repositories;

namespace Application.Users.Queries.GetUserById
{
    public class GetUserByIdQueryValidator : IAsyncValidator<GetUserByIdQuery>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdQueryValidator( IUserRepository userRepository )
        {
            _userRepository = userRepository;
        }

        public async Task<ValidationResult> ValidationAsync( GetUserByIdQuery query )
        {
            if( !await _userRepository.ContainsAsync( user => user.Id == query.Id ) )
            {
                return ValidationResult.Fail( "Пользователя с таким id нет" );
            }

            return ValidationResult.Ok();
        }
    }
}
