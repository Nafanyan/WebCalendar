using Application.Validation;
using Domain.Entities;
using Domain.Repositories;

namespace Application.UserAuthorizationTokens
{
    public class AuthorizationUserQueryValidator : IAsyncValidator<AuthorizationUserQuery>
    {
        private readonly IUserAuthorizationRepository _userAuthorizationRepository;
        private readonly IUserRepository _userRepository;

        public AuthorizationUserQueryValidator(IUserAuthorizationRepository userAuthorizationRepository, IUserRepository userRepository)
        {
            _userAuthorizationRepository = userAuthorizationRepository;
            _userRepository = userRepository;
        }

        public async Task<ValidationResult> ValidationAsync(AuthorizationUserQuery query)
        {
            //if (query.Login == null)
            //{
            //    return ValidationResult.Fail("The login cannot be empty/cannot be null");
            //}

            //if (!await _userRepository.ContainsAsync(user => user.Login == query.Login))
            //{
            //    return ValidationResult.Fail("There is no user with this username");
            //}

            //User user = await _userRepository.GetByIdAsync(query.UserId);
            //if (user.PasswordHash != query.PasswordHash)
            //{
            //    return ValidationResult.Fail("The entered password does not match the current one");
            //}

            //if (!await _userAuthorizationRepository.ContainsAsync(token => token.UserId == query.UserId))
            //{
            //    return ValidationResult.Fail("The user is not registered");
            //}

            return ValidationResult.Ok();
        }
    }
}
