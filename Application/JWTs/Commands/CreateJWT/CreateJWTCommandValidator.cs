using Application.Validation;
using Domain.Repositories;

namespace Application.JWTs.Commands.CreateJWT
{
    public class CreateJWTCommandValidator : IAsyncValidator<CreateJWTCommand>
    {
        private readonly IJWTRepository _jWTRepository;

        public CreateJWTCommandValidator (IJWTRepository jWTRepository)
        {
            _jWTRepository = jWTRepository;
        }

        public async Task<ValidationResult> ValidationAsync(CreateJWTCommand command)
        {
            if (await _jWTRepository.ContainsAsync(token => token.UserId == command.UserId))
            {
                return ValidationResult.Fail("Such a user is already registered");
            }
            return ValidationResult.Ok();
        }
    }
}
