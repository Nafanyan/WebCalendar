using Application.Events.Queries.GetEvent;
using Application.Validation;
using Domain.Repositories;

namespace Application.Users.Queries.GetEvents
{
    public class GetEventsQueryValidation : IValidator<GetEventsQuery>
    {
        private readonly IUserRepository _userRepository;

        public GetEventsQueryValidation(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ValidationResult Validation(GetEventsQuery query)
        {
            string error = "No errors";
            if (_userRepository.GetById(query.UserId) == null)
            {
                error = "There is no user with this id";
                return new ValidationResult(true, error);
            }

            return new ValidationResult(false, error);
        }
    }
}
