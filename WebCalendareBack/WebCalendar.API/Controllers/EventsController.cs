using Microsoft.AspNetCore.Mvc;
using WebCalendar.Domain.Events;

namespace WebCalendar.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventsController : ControllerBase
    {
        public List<Event> events = new List<Event>();
        public EventsController()
        {
            events.Add(new Event("1", "2", new DateTime(2023,4,25,10,10,00), new DateTime(2023, 4, 25, 12, 10, 00)));
            events.Add(new Event("1", "2", new DateTime(2023, 4, 26, 10, 10, 00), new DateTime(2023, 4, 26, 12, 10, 00)));
            events.Add(new Event("1", "2", new DateTime(2023, 4, 27, 10, 10, 00), new DateTime(2023, 4, 27, 12, 10, 00)));
            events.Add(new Event("1", "2", new DateTime(2023, 4, 28, 10, 10, 00), new DateTime(2023, 4, 28, 12, 10, 00)));
            events.Add(new Event("1", "2", new DateTime(2023, 4, 29, 10, 10, 00), new DateTime(2023, 4, 29, 12, 10, 00)));


        }

        [HttpGet()]
        public IActionResult GetEvents()
        {
            return Ok(events);
        }

        [HttpGet("mounths/{year}/{mounth}")]
        public IActionResult GetEventsMounth([FromRoute] int year, int mounth)
        {
            return Ok(events.Where(e => e.StartEvent.Year == year)
                .Where(e => e.StartEvent.Month == mounth));
        }

        [HttpGet("weeks/{year}/{mounth}/{day}")]
        public IActionResult GetEventsWeek([FromRoute] int year, int mounth, int day)
        {
            int numWeek = day % 7;
            return Ok(events.Where(e => e.StartEvent.Year == year)
                .Where(e => e.StartEvent.Month == mounth)
                .Where(e => e.StartEvent.Day > numWeek * 7  && day <= numWeek * 7 + 7));
        }

        [HttpGet("days/{year}/{mounth}/{day}")]
        public IActionResult GetEbentsDay([FromRoute] int year, int mounth, int day)
        {
            return Ok(events.Where(e => e.StartEvent.Year == year)
                .Where(e => e.StartEvent.Month == mounth)
                .Where(e => e.StartEvent.Day == day));
        }
    }
}
