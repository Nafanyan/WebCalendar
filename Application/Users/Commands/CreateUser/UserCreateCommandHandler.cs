using Application.Interfaces;
using Application.Result;
using Application.Validation;
using Domain.Entities;
using Domain.Repositories;
using Domain.UnitOfWork;

namespace Application.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAsyncValidator<CreateUserCommand> _addUserCommandValidator;
        private readonly IUnitOfWork _unitOfWork;

        public CreateUserCommandHandler(
            IUserRepository userRepository, 
            IAsyncValidator<CreateUserCommand> validator,
            IUnitOfWork unitOfWork) 
        {
            _userRepository = userRepository;
            _addUserCommandValidator = validator;
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandResult> HandleAsync(CreateUserCommand addUserCommand)
        {
            ValidationResult validationResult = await _addUserCommandValidator.ValidationAsync(addUserCommand);
            if (!validationResult.IsFail)
            {
                User user = new User(addUserCommand.Login, addUserCommand.PasswordHash);
                await _userRepository.AddAsync(user);
                await _unitOfWork.CommitAsync();
            }
            return new CommandResult(validationResult);
        }
    }
}
