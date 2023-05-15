using Application.Result;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Users.Commands.UpdateUserLogin
{
    public class UpdateUserLoginCommandHandler : BaseUserUseCase, IUserCommandHandler<UpdateUserLoginCommand>
    {
        UpdateUserLoginCommandValidation _updateUserLoginCommandValidation;

        public UpdateUserLoginCommandHandler(IUserRepository userRepository) : base(userRepository)
        {
            _updateUserLoginCommandValidation = new UpdateUserLoginCommandValidation(userRepository);
        }

        public async Task<ResultCommand> Handle(UpdateUserLoginCommand updateUserLoginCommand)
        {
            string msg = _updateUserLoginCommandValidation.Validation(updateUserLoginCommand);
            if (msg == "Ok")
            {
                User user = await _userRepository.GetById(updateUserLoginCommand.Id);
                user.SetLogin(updateUserLoginCommand.Login);
            }
            return new ResultCommand(msg);
        }
    }
}
