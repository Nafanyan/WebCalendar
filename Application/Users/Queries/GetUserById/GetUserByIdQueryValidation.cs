using Application.Validation;
using Domain.Repositories;

namespace Application.Users.Queries.GetUserById
{
    public class GetUserByIdQueryValidation : IAsyncValidator<GetUserByIdQuery>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdQueryValidation(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ValidationResult> Validation(GetUserByIdQuery query)
        {
            string error;
            if (!await _userRepository.Contains(query.Id))
            {
                error = "There is no user with this id";
                return ValidationResult.Fail(error);
            }

            return ValidationResult.Ok();
        }
    }
}
