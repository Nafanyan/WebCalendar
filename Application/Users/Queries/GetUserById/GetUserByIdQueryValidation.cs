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
            if (!await _userRepository.Contains(user => user.Id == query.Id))
            {
                return ValidationResult.Fail("There is no user with this id");
            }

            return ValidationResult.Ok();
        }
    }
}
