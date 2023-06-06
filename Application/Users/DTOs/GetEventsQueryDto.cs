using Domain.Entities;

namespace Application.Users.DTOs
{
    public class GetEventsQueryDto
    {
        public List<Event> events {get; init;}
    }
}
