using Application.CQRSInterfaces;
using Application.Result;
using Application.Validation;
using Application.Entities;
using Application.Repositories;

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
                _userRepository.Add(user);
                await _unitOfWork.CommitAsync();
            }
            return new CommandResult(validationResult);
        }
    }
}
