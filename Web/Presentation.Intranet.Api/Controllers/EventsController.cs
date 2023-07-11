using Application.Events.Commands.CreateEvent;
using Application.Events.Commands.DeleteEvent;
using Application.Events.Commands.UpdateEvent;
using Application.Events.DTOs;
using Application.Events.Queries.EventQuery;
using Application.Interfaces;
using Application.Result;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Presentation.Intranet.Api.Dtos.EventRequest;
using Presentation.Intranet.Api.Mappers.EventMappers;

namespace Presentation.Intranet.Api.Controllers
{
    [ApiController]
    [Route("api/Users")]
    public class EventsController : ControllerBase
    {
        private readonly ICommandHandler<EventCreateCommand> _createEventCommandHandler;
        private readonly ICommandHandler<EventDeleteCommand> _deleteEventCommandHandler;
        private readonly ICommandHandler<EventUpdateCommand> _updateEventCommandHandler;
        private readonly IQueryHandler<EventQueryDto, EventQuery> _eventQueryHandler;

        public EventsController(
            ICommandHandler<EventCreateCommand> createEventCommandHandler,
            ICommandHandler<EventDeleteCommand> deleteEventCommandHandler,
            ICommandHandler<EventUpdateCommand> updateEventCommandHandler,
            IQueryHandler<EventQueryDto, EventQuery> getEventQueryHandler)
        {
            _createEventCommandHandler = createEventCommandHandler;
            _deleteEventCommandHandler = deleteEventCommandHandler;
            _updateEventCommandHandler = updateEventCommandHandler;
            _eventQueryHandler = getEventQueryHandler;
        }

        [HttpGet("{userId:long}/[controller]")]
        public async Task<IActionResult> GetEvent([FromRoute] long userId, DateTime startEvent, DateTime endEvent)
        {
            EventQuery getEventQuery = new EventQuery
            {
                UserId = userId,
                StartEvent = startEvent,
                EndEvent = endEvent
            };

            QueryResult<EventQueryDto> queryResult = await _eventQueryHandler.HandleAsync(getEventQuery);
            if (queryResult.ValidationResult.IsFail)
            {
                return BadRequest(queryResult);
            }
            return Ok(queryResult);
        }

        [HttpPost("{userId:long}/[controller]")]
        public async Task<IActionResult> CreateEvent([FromRoute] long userId, [FromBody] CreateEventDto createEventRequest)
        {
            CommandResult commandResult = await _createEventCommandHandler.HandleAsync(createEventRequest.Map(userId));
            if (commandResult.ValidationResult.IsFail)
            {
                return BadRequest(commandResult);
            }
            return Ok();
        }

        [HttpDelete("{userId:long}/[controller]")]
        public async Task<IActionResult> DeleteEvent([FromRoute]long userId, [FromBody] DeleteEventDto deleteEventRequest)
        {
            CommandResult commandResult = await _deleteEventCommandHandler.HandleAsync(deleteEventRequest.Map(userId));
            if (commandResult.ValidationResult.IsFail)
            {
                return BadRequest(commandResult);
            }
            return Ok();
        }

        [HttpPut("{userId:long}/[controller]")]
        public async Task<IActionResult> UpdateEvent([FromRoute] long userId, [FromBody] UpdateEventDto updateEventRequest)
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
