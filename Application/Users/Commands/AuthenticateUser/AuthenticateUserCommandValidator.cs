﻿using Application.Validation;
using Domain.Entities;
using Domain.Repositories;

namespace Application.UserAuthorizationTokens.Commands.AuthenticateUser
{
    public class AuthenticateUserCommandValidator : IAsyncValidator<AuthenticateUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public AuthenticateUserCommandValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ValidationResult> ValidationAsync(AuthenticateUserCommand command)
        {
            if (command.Login == null || command.Login == String.Empty)
            {
                return ValidationResult.Fail("The login cannot be empty/cannot be null");
            }

            if (!await _userRepository.ContainsAsync(user => user.Login == command.Login && user.PasswordHash == command.PasswordHash))
            {
                return ValidationResult.Fail("Invalid username or password");
            }

            return ValidationResult.Ok();
        }
    }
}
