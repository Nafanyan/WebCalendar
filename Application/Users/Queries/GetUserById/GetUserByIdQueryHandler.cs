using Domain.Entities;
using Domain.Repositories;

namespace Application.Users.Queries.GetUserById
{
    public interface IGetUserByIdQueryHandler
    {
        User Execute(GetUserByIdQuery getUserByIdQuery);
    }

    public class GetUserByIdQueryHandler : BaseUserUseCase, IGetUserByIdQueryHandler
    {
        public GetUserByIdQueryHandler(IUserRepository userRepository) : base(userRepository)
        {
        }

        public User Execute(GetUserByIdQuery getUserByIdQuery)
        {
            QueryValidation(getUserByIdQuery);
            return _userRepository.GetById(getUserByIdQuery.Id).Result;
        }
        private void QueryValidation(GetUserByIdQuery getUserByIdQuery)
        {
            _validationUser.ValueNotFound(getUserByIdQuery.Id);
        }
    }
}
