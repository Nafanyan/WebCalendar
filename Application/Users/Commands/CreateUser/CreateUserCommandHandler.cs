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
        private readonly IValidator<CreateUserCommand> _addUserCommandValidation;

        public CreateUserCommandHandler(IUserRepository userRepository, IValidator<CreateUserCommand> validator) 
        {
            _userRepository = userRepository;
            _addUserCommandValidation = validator;
        }

        public async Task<CommandResult> Handle(CreateUserCommand addUserCommand)
        {
            ValidationResult validationResult = _addUserCommandValidation.Validation(addUserCommand);
            if (!validationResult.IsFail)
            {
                User user = new User(addUserCommand.Login, addUserCommand.PasswordHash);
                await _userRepository.Add(user);
            }
            return new CommandResult(validationResult);
        }
    }
}
