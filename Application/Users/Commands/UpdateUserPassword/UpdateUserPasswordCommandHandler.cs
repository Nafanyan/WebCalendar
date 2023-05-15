using Application.Result;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Users.Commands.UpdateUserPassword
{
    public class UpdateUserPasswordCommandHandler : IUserCommandHandler<UpdateUserPasswordCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly UpdateUserPasswordCommandValidation _updateUserPasswordCommandValidation;

        public UpdateUserPasswordCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _updateUserPasswordCommandValidation = new UpdateUserPasswordCommandValidation(userRepository);
        }

        public async Task<ResultCommand> Handle(UpdateUserPasswordCommand updateUserPasswordCommand)
        {
            string msg = _updateUserPasswordCommandValidation.Validation(updateUserPasswordCommand);
            if (msg == "Ok")
            {
                User user = await _userRepository.GetById(updateUserPasswordCommand.Id);
                user.SetPasswordHash(updateUserPasswordCommand.NewPasswordHash);
            }
            return new ResultCommand(msg);
        }
    }
}
