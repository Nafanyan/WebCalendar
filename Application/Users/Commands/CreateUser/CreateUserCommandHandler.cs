using Application.Interfaces;
using Application.Result;
using Application.Validation;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAsyncValidator<CreateUserCommand> _addUserCommandValidator;

        public CreateUserCommandHandler(IUserRepository userRepository, IAsyncValidator<CreateUserCommand> validator) 
        {
            _userRepository = userRepository;
            _addUserCommandValidator = validator;
        }

        public async Task<CommandResult> HandleAsync(CreateUserCommand addUserCommand)
        {
            ValidationResult validationResult = await _addUserCommandValidator.ValidationAsync(addUserCommand);
            if (!validationResult.IsFail)
            {
                User user = new User(addUserCommand.Login, addUserCommand.PasswordHash);
                await _userRepository.AddAsync(user);
            }
            return new CommandResult(validationResult);
        }
    }
}
