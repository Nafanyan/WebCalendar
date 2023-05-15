using Domain.Repositories;

namespace Application.Users.Queries.GetUserById
{
    public class GetUserByIdQueryValidation : IValidation<GetUserByIdQuery>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdQueryValidation(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public string Validation(GetUserByIdQuery query)
        {
            if (_userRepository.GetById(query.Id) == null)
            {
                return "There is no user with this id";
            }

            return "Ok";
        }
    }
}
