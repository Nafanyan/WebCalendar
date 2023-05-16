using Application.Interfaces;
using Application.Result;
using Application.Validation;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Users.Commands.UpdateUserLogin
{
    public class UpdateUserLoginCommandHandler : ICommandHandler<UpdateUserLoginCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly UpdateUserLoginCommandValidation _updateUserLoginCommandValidation;

        public UpdateUserLoginCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _updateUserLoginCommandValidation = new UpdateUserLoginCommandValidation(userRepository);
        }

        public async Task<CommandResult> Handle(UpdateUserLoginCommand updateUserLoginCommand)
        {
            ValidationResult validationResult= _updateUserLoginCommandValidation.Validation(updateUserLoginCommand);
            if (!validationResult.IsFail)
            {
                User user = await _userRepository.GetById(updateUserLoginCommand.Id);
                user.SetLogin(updateUserLoginCommand.Login);
            }
            return new CommandResult(validationResult);
        }
    }
}
