﻿using Application.Validation;
using Domain.Repositories;

namespace Application.Users.Queries.GetUserById
{
    public class GetUserByIdQueryValidator : IAsyncValidator<GetUserByIdQuery>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdQueryValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ValidationResult> ValidationAsync(GetUserByIdQuery query)
        {
            if (!await _userRepository.ContainsAsync(user => user.Id == query.Id))
            {
                return ValidationResult.Fail("There is no user with this id");
            }

            return ValidationResult.Ok();
        }
    }
}
