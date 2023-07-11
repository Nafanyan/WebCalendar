using Application.Validation;
using Domain.Repositories;

namespace Application.Users.Queries.QueryUserById
{
    public class UserQueryValidatorById : IAsyncValidator<UserQueryById>
    {
        private readonly IUserRepository _userRepository;

        public UserQueryValidatorById(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ValidationResult> ValidationAsync(UserQueryById query)
        {
            if (!await _userRepository.ContainsAsync(user => user.Id == query.Id))
            {
                return ValidationResult.Fail("There is no user with this id");
            }

            return ValidationResult.Ok();
        }
    }
}
