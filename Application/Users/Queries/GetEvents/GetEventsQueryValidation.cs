using Application.Events.Queries.GetEvent;
using Application.Validation;
using Domain.Repositories;

namespace Application.Users.Queries.GetEvents
{
    public class GetEventsQueryValidation : IAsyncValidator<GetEventsQuery>
    {
        private readonly IUserRepository _userRepository;

        public GetEventsQueryValidation(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ValidationResult> ValidationAsync(GetEventsQuery query)
        {
            if (!await _userRepository.ContainsAsync(user => user.Id == query.UserId))
            {
                return ValidationResult.Fail("There is no user with this id");
            }

            return ValidationResult.Ok();
        }
    }
}
