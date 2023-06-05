using Application.Events.Commands.CreateEvent;
using Application.Events.Commands.DeleteEvent;
using Application.Events.Commands.UpdateEvent;
using Application.Events.Queries.GetEvent;
using Application.Interfaces;
using Application.Result;
using Domain.Entities;
using Domain.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Presentation.Intranet.Api.Dtos.EventRequest;
using Presentation.Intranet.Api.Mappers.EventMappers;

namespace Presentation.Intranet.Api.Controllers
{
    [ApiController]
    [Route("Users")]
    public class EventsController : ControllerBase
    {
        private readonly ICommandHandler<CreateEventCommand> _createEventCommandHandler;
        private readonly ICommandHandler<DeleteEventCommand> _deleteEventCommandHandler;
        private readonly ICommandHandler<UpdateEventCommand> _updateEventCommandHandler;
        private readonly IQueryHandler<Event, GetEventQuery> _getEventQueryHandler;

        public EventsController(ICommandHandler<CreateEventCommand> createEventCommandHandler,
            ICommandHandler<DeleteEventCommand> deleteEventCommandHandler,
            ICommandHandler<UpdateEventCommand> updateEventCommandHandler,
            IQueryHandler<Event, GetEventQuery> getEventQueryHandler)
        {
            _createEventCommandHandler = createEventCommandHandler;
            _deleteEventCommandHandler = deleteEventCommandHandler;
            _updateEventCommandHandler = updateEventCommandHandler;
            _getEventQueryHandler = getEventQueryHandler;
        }

        [HttpGet("{userId:long}/[controller]")]
        public async Task<IActionResult> GetEvent([FromRoute] long userId, string startEvent, string endEvent)
        {
            GetEventQuery getEventQuery = new GetEventQuery
            {
                UserId = userId,
                StartEvent = startEvent,
                EndEvent = endEvent
            };

            QueryResult<Event> queryResult = await _getEventQueryHandler.HandleAsync(getEventQuery);
            if (queryResult.ValidationResult.IsFail)
            {
                return BadRequest(queryResult);
            }
            return Ok(queryResult);
        }

        [HttpPost("{userId:long}/[controller]")]
        public async Task<IActionResult> CreateEvent([FromRoute] long userId, [FromBody] CreateEventRequest createEventRequest)
        {
            CommandResult commandResult = await _createEventCommandHandler.HandleAsync(createEventRequest.Map(userId));
            if (commandResult.ValidationResult.IsFail)
            {
                return BadRequest(commandResult);
            }
            return Ok();
        }

        [HttpDelete("{userId:long}/[controller]")]
        public async Task<IActionResult> DeleteEvent([FromRoute]long userId, [FromBody] DeleteEventRequest deleteEventRequest)
        {
            CommandResult commandResult = await _deleteEventCommandHandler.HandleAsync(deleteEventRequest.Map(userId));
            if (commandResult.ValidationResult.IsFail)
            {
                return BadRequest(commandResult);
            }
            return Ok();
        }

        [HttpPut("{userId:long}/[controller]")]
        public async Task<IActionResult> UpdateEvent([FromRoute] long userId, [FromBody] UpdateEventRequest updateEventRequest)
        {
            CommandResult commandResult = await _updateEventCommandHandler.HandleAsync(updateEventRequest.Map(userId));
            if (commandResult.ValidationResult.IsFail)
            {
                return BadRequest(commandResult.ValidationResult);
            }
            return Ok();
        }
    }
}
