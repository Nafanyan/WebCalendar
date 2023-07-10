using Application.Validation;
using Domain.Repositories;

namespace Application.UserAuthorizationTokens.Commands.RegistrationUser
{
    public class RegistrationUserCommandValidator : IAsyncValidator<RegistrationUserCommand>
    {
        private readonly IUserAuthorizationRepository _userAuthorizationRepository;

        public RegistrationUserCommandValidator(IUserAuthorizationRepository userAuthorizationRepository)
        {
            _userAuthorizationRepository = userAuthorizationRepository;
        }

        public async Task<ValidationResult> ValidationAsync(RegistrationUserCommand command)
        {
            if (await _userAuthorizationRepository.ContainsAsync(token => token.UserId == command.UserId))
            {
                return ValidationResult.Fail("The user is already registered");
            }
            return ValidationResult.Ok();
        }
    }
}
