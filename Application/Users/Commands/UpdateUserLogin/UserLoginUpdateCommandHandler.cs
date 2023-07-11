using Application.Interfaces;
using Application.Result;
using Application.Validation;
using Domain.Entities;
using Domain.Repositories;
using Domain.UnitOfWork;

namespace Application.Users.Commands.UpdateUserLogin
{
    public class UserLoginUpdateCommandHandler : ICommandHandler<UserLoginUpdateCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAsyncValidator<UserLoginUpdateCommand> _updateUserLoginCommandValidator;
        private readonly IUnitOfWork _unitOfWork;

        public UserLoginUpdateCommandHandler(
            IUserRepository userRepository, 
            IAsyncValidator<UserLoginUpdateCommand> validator,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _updateUserLoginCommandValidator = validator;
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandResult> HandleAsync(UserLoginUpdateCommand updateUserLoginCommand)
        {
            ValidationResult validationResult= await _updateUserLoginCommandValidator.ValidationAsync(updateUserLoginCommand);
            if (!validationResult.IsFail)
            {
                User user = await _userRepository.GetByIdAsync(updateUserLoginCommand.Id);
                user.SetLogin(updateUserLoginCommand.Login);
                await _unitOfWork.CommitAsync();
            }
            return new CommandResult(validationResult);
        }
    }
}
