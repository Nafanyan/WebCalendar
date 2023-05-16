using Application.Validation;
using Domain.Repositories;

namespace Application.Users.Queries.GetUserById
{
    public class GetUserByIdQueryValidation : IValidator<GetUserByIdQuery>, IAsyncValidator<GetUserByIdQuery>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdQueryValidation(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ValidationResult Validation(GetUserByIdQuery query)
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
        public async Task<ValidationResult> AsyncValidation(GetUserByIdQuery query)
        {
            string error = "No errors";
            ValidationResult validationResult = new ValidationResult(false, error);

            if (await _userRepository.GetById(query.Id) == null)
            {
                error = "There is no user with this id";
                validationResult = new ValidationResult(true, error);
                return validationResult;
            }

            return validationResult;
        }
    }
}
