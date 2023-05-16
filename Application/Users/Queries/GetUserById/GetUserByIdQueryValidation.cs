using Application.Validation;
using Domain.Repositories;

namespace Application.Users.Queries.GetUserById
{
    public class GetUserByIdQueryValidation : IValidation<GetUserByIdQuery>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdQueryValidation(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ValidationResult Validation(GetUserByIdQuery query)
        {
            string error = "No errors";
            if (_userRepository.GetById(query.Id) == null)
            {
                error = "There is no user with this id";
                return new ValidationResult(true, error);
            }

            return new ValidationResult(false, error);
        }
    }
}
