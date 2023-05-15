using Application.Events.Queries.GetEvent;
using Domain.Repositories;

namespace Application.Users.Queries.GetEvents
{
    public class GetEventsQueryValidation : IValidation<GetEventsQuery>
    {
        private readonly IUserRepository _userRepository;

        public GetEventsQueryValidation(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public string Validation(GetEventsQuery query)
        {
            if (_userRepository.GetById(query.UserId) == null)
            {
                return "There is no user with this id";
            }

            return "Ok";
        }
    }
}
