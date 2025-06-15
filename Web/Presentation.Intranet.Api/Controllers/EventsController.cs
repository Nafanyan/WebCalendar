using Application.Events.Commands.CreateEvent;
using Application.Events.Commands.DeleteEvent;
using Application.Events.Commands.UpdateEvent;
using Application.Events.DTOs;
using Application.Events.Queries.GetEvent;
using Application.Interfaces;
using Application.Result;
using Microsoft.AspNetCore.Mvc;
using Presentation.Intranet.Api.Dtos.EventRequest;
using Infrastructure.JwtAuthorizations;

namespace Presentation.Intranet.Api.Controllers
{
    [ApiController]
    [Route( "Api/Users" )]
    [JwtAuthorization]
    public class EventsController : ControllerBase
    {
        private readonly ICommandHandler<CreateEventCommand> _createEventCommandHandler;
        private readonly ICommandHandler<DeleteEventCommand> _deleteEventCommandHandler;
        private readonly ICommandHandler<UpdateEventCommand> _updateEventCommandHandler;
        private readonly IQueryHandler<GetEventQueryDto, GetEventQuery> _getEventQueryHandler;

        public EventsController(
            ICommandHandler<CreateEventCommand> createEventCommandHandler,
            ICommandHandler<DeleteEventCommand> deleteEventCommandHandler,
            ICommandHandler<UpdateEventCommand> updateEventCommandHandler,
            IQueryHandler<GetEventQueryDto, GetEventQuery> getEventQueryHandler )
        {
            _createEventCommandHandler = createEventCommandHandler;
            _deleteEventCommandHandler = deleteEventCommandHandler;
            _updateEventCommandHandler = updateEventCommandHandler;
            _getEventQueryHandler = getEventQueryHandler;
        }

        [HttpGet( "{userId}/[controller]" )]
        public async Task<IActionResult> GetEvent( [FromRoute] long userId, DateTime startEvent, DateTime endEvent )
        {
            GetEventQuery getEventQuery = new GetEventQuery
            {
                UserId = userId,
                StartEvent = startEvent,
                EndEvent = endEvent
            };

            QueryResult<GetEventQueryDto> queryResult = await _getEventQueryHandler.HandleAsync( getEventQuery );
            if( queryResult.ValidationResult.IsFail )
            {
                return BadRequest( queryResult );
            }
            return Ok( queryResult );
        }

        [HttpPost( "{userId}/[controller]" )]
        public async Task<IActionResult> CreateEvent( [FromRoute] long userId, [FromBody] CreateEventDto createEventRequest )
        {
            CreateEventCommand createEventCommand = new CreateEventCommand
            {
                UserId = userId,
                Name = createEventRequest.Name,
                Description = createEventRequest.Description,
                StartEvent = createEventRequest.StartEvent,
                EndEvent = createEventRequest.EndEvent
            };
            CommandResult commandResult = await _createEventCommandHandler.HandleAsync( createEventCommand );

            if( commandResult.ValidationResult.IsFail )
            {
                return BadRequest( commandResult );
            }
            return Ok( commandResult );
        }

        [HttpDelete( "{userId}/[controller]" )]
        public async Task<IActionResult> DeleteEvent( [FromRoute] long userId, [FromBody] DeleteEventDto deleteEventRequest )
        {
            DeleteEventCommand deleteEventCommand = new DeleteEventCommand
            {
                UserId = userId,
                StartEvent = deleteEventRequest.StartEvent,
                EndEvent = deleteEventRequest.EndEvent
            };
            CommandResult commandResult = await _deleteEventCommandHandler.HandleAsync( deleteEventCommand );

            if( commandResult.ValidationResult.IsFail )
            {
                return BadRequest( commandResult );
            }
            return Ok( commandResult );
        }

        [HttpPut( "{userId}/[controller]" )]
        public async Task<IActionResult> UpdateEvent( [FromRoute] long userId, [FromBody] UpdateEventDto updateEventRequest )
        {
            UpdateEventCommand updateEventCommand = new UpdateEventCommand
            {
                UserId = userId,
                Name = updateEventRequest.Name,
                Description = updateEventRequest.Description,
                StartEvent = updateEventRequest.StartEvent,
                EndEvent = updateEventRequest.EndEvent
            };
            CommandResult commandResult = await _updateEventCommandHandler.HandleAsync( updateEventCommand );

            if( commandResult.ValidationResult.IsFail )
            {
                return BadRequest( commandResult.ValidationResult );
            }
            return Ok( commandResult );
        }
    }
}
