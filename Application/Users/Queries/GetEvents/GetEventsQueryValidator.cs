using Application.Validation;
using Application.Repositories;

namespace Application.Users.Queries.GetEvents
{
    public class GetEventsQueryValidator : IAsyncValidator<GetEventsQuery>
    {
        private readonly IUserRepository _userRepository;

        public GetEventsQueryValidator( IUserRepository userRepository )
        {
            _userRepository = userRepository;
        }

        public async Task<ValidationResult> ValidationAsync( GetEventsQuery query )
        {
            if( !await _userRepository.ContainsAsync( user => user.Id == query.UserId ) )
            {
                return ValidationResult.Fail( "Пользователя с таким id нет" );
            }

            return ValidationResult.Ok();
        }
    }
}
