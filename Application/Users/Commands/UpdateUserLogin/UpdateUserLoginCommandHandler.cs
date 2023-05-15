using Application.Result;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Users.Commands.UpdateUserLogin
{
    public class UpdateUserLoginCommandHandler : IUserCommandHandler<UpdateUserLoginCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly UpdateUserLoginCommandValidation _updateUserLoginCommandValidation;

        public UpdateUserLoginCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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
