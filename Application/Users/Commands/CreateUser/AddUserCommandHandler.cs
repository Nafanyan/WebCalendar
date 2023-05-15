using Application.Result;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Users.Commands.CreateUser
{
    public class AddUserCommandHandler : BaseUserUseCase, IUserCommandHandler<AddUserCommand>
    {
        private readonly AddUserCommandValidation _addUserCommandValidation;

        public AddUserCommandHandler(IUserRepository userRepository) : base(userRepository)
        {
            _addUserCommandValidation = new AddUserCommandValidation(userRepository);
        }

        public async Task<ResultCommand> Handle(AddUserCommand addUserCommand)
        {
            string msg = _addUserCommandValidation.Validation(addUserCommand);
            if (msg == "Ok")
            {
                User user = new User(addUserCommand.Login, addUserCommand.PasswordHash);
                await _userRepository.Add(user);
            }
            return new ResultCommand(msg);
        }
    }
}
