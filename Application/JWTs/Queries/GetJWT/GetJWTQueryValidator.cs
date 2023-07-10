using Application.Validation;
using Domain.Repositories;

namespace Application.JWTs.Queries.GetJWT
{
    public class GetJWTQueryValidator : IAsyncValidator<GetJWTQuery>
    {
        private readonly IJWTRepository _jWTRepository;

        public GetJWTQueryValidator(IJWTRepository jWTRepository)
        {
            _jWTRepository = jWTRepository;
        }

        public async Task<ValidationResult> ValidationAsync(GetJWTQuery query)
        {
            if (! await _jWTRepository.ContainsAsync(token => token.UserId == query.UserId))
            {
                ValidationResult.Fail("There is no such user in the database");
            }

            if (! await _jWTRepository.ContainsAsync(token => token.RefreshToken == query.RefreshJWT))
            {
                ValidationResult.Fail("The token is outdated, perform authorization");
            }

            return ValidationResult.Ok();
        }
    }
}
