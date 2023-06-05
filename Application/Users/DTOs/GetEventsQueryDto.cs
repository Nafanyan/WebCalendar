using Domain.Entities;

namespace Application.Users.DTOs
{
    public class GetEventsQueryDto
    {
        public IReadOnlyList<Event> events {get; init;}
    }
}
