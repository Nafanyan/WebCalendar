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

        public async Task<ValidationResult> Validation(GetEventsQuery query)
        {
            string error;
            if (!await _userRepository.Contains(query.UserId))
            {
                error = "There is no user with this id";
                return ValidationResult.Fail(error);
            }

            return ValidationResult.Ok();
        }
    }
}
