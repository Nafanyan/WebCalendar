﻿using Application.Validation;
using Domain.Entities;
using Domain.Repositories;

namespace Application.UserAuthorizationTokens.Commands.RefreshToken
{
    public class RefreshTokenCommandValidator : IAsyncValidator<RefreshTokenCommand>
    {
        private readonly IUserAuthorizationTokenRepository _userAuthorizationTokenRepository;

        public RefreshTokenCommandValidator(IUserAuthorizationTokenRepository userAuthorizationTokenRepository)
        {
            _userAuthorizationTokenRepository = userAuthorizationTokenRepository;
        }

        public async Task<ValidationResult> ValidationAsync(RefreshTokenCommand command)
        {
            UserAuthorizationToken token = await _userAuthorizationTokenRepository.GetTokenByRefreshTokenAsync(command.RefreshToken);

            if (token is null)
            {
                return ValidationResult.Fail("Authorization required");
            }

            if (DateTime.Now > token.ExpiryDate)
            {
                return ValidationResult.Fail("Token expired");
            }

            if (token.RefreshToken != command.RefreshToken)
            {
                return ValidationResult.Fail("Token is not valid");
            }

            return ValidationResult.Ok();
        }
    }
}