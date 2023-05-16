using Application.Events.Queries.GetEvent;
using Application.Validation;
using Domain.Repositories;

namespace Application.Users.Queries.GetEvents
{
    public class GetEventsQueryValidation : IValidator<GetEventsQuery>, IAsyncValidator<GetEventsQuery>
    {
        private readonly IUserRepository _userRepository;

        public GetEventsQueryValidation(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ValidationResult Validation(GetEventsQuery query)
        {
            string error = "No errors";
            ValidationResult validationResult = new ValidationResult(false, error);

            validationResult = AsyncValidation(query).Result;
            if (validationResult.IsFail)
            {
                return validationResult;
            }

            return validationResult;
        }
        public async Task<ValidationResult> AsyncValidation(GetEventsQuery query)
        {
            string error = "No errors";
            ValidationResult validationResult = new ValidationResult(false, error);

            if (await _userRepository.GetById(query.UserId) == null)
            {
                error = "There is no user with this id";
                validationResult = new ValidationResult(true, error);
                return validationResult;
            }

            return validationResult;
        }
    }
}
