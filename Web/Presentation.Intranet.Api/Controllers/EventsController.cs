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
        private readonly IUnitOfWork _unitOfWork;

        public EventsController(ICommandHandler<CreateEventCommand> createEventCommandHandler,
            ICommandHandler<DeleteEventCommand> deleteEventCommandHandler,
            ICommandHandler<UpdateEventCommand> updateEventCommandHandler,
            IQueryHandler<Event, GetEventQuery> getEventQueryHandler,
            IUnitOfWork unitOfWork)
        {
            _createEventCommandHandler = createEventCommandHandler;
            _deleteEventCommandHandler = deleteEventCommandHandler;
            _updateEventCommandHandler = updateEventCommandHandler;
            _getEventQueryHandler = getEventQueryHandler;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{userId:long}/[controller]")]
        public async Task<IActionResult> GetEvent([FromRoute] long userId, string startEvent, string endEvent)
        {
            if (startEvent.StringValidationToDate().IsFail )
            {
                return BadRequest(startEvent.StringValidationToDate().Error);
            }
            if (endEvent.StringValidationToDate().IsFail)
            {
                return BadRequest(endEvent.StringValidationToDate().Error);
            }

            GetEventQuery getEventQuery = new GetEventQuery
            {
                UserId = userId,
                StartEvent = startEvent.MapStringToDate(),
                EndEvent = endEvent.MapStringToDate()
            };
            QueryResult<Event> queryResult = await _getEventQueryHandler.HandleAsync(getEventQuery);

            if (queryResult.ValidationResult.IsFail)
            {
                return BadRequest(queryResult.ValidationResult);
            }

            return Ok(queryResult.ObjResult);
        }

        [HttpPost("{userId:long}/[controller]")]
        public async Task<IActionResult> CreateEvent([FromRoute] long userId, [FromBody] CreateEventRequest createEventRequest)
        {
            if (createEventRequest.StartEvent.StringValidationToDate().IsFail)
            {
                return BadRequest(createEventRequest.StartEvent.StringValidationToDate().Error);
            }
            if (createEventRequest.EndEvent.StringValidationToDate().IsFail)
            {
                return BadRequest(createEventRequest.EndEvent.StringValidationToDate().Error);
            }

            CommandResult commandResult = await _createEventCommandHandler.HandleAsync(createEventRequest.Map(userId));
            if (commandResult.ValidationResult.IsFail)
            {
                return BadRequest(commandResult.ValidationResult);
            }

            await _unitOfWork.CommitAsync();
            return Ok();
        }

        [HttpDelete("{userId:long}/[controller]")]
        public async Task<IActionResult> DeleteEvent([FromRoute]long userId, [FromBody] DeleteEventRequest deleteEventRequest)
        {
            if (deleteEventRequest.StartEvent.StringValidationToDate().IsFail)
            {
                return BadRequest(deleteEventRequest.StartEvent.StringValidationToDate().Error);
            }
            if (deleteEventRequest.EndEvent.StringValidationToDate().IsFail)
            {
                return BadRequest(deleteEventRequest.EndEvent.StringValidationToDate().Error);
            }

            CommandResult commandResult = await _deleteEventCommandHandler.HandleAsync(deleteEventRequest.Map(userId));
            if (commandResult.ValidationResult.IsFail)
            {
                return BadRequest(commandResult.ValidationResult);
            }

            await _unitOfWork.CommitAsync();
            return Ok();
        }

        [HttpPut("{userId:long}/[controller]")]
        public async Task<IActionResult> UpdateEvent([FromRoute] long userId, [FromBody] UpdateEventRequest updateEventRequest)
        {
            if (updateEventRequest.StartEvent.StringValidationToDate().IsFail)
            {
                return BadRequest(updateEventRequest.StartEvent.StringValidationToDate().Error);
            }
            if (updateEventRequest.EndEvent.StringValidationToDate().IsFail)
            {
                return BadRequest(updateEventRequest.EndEvent.StringValidationToDate().Error);
            }

            CommandResult commandResult = await _updateEventCommandHandler.HandleAsync(updateEventRequest.Map(userId));
            if (commandResult.ValidationResult.IsFail)
            {
                return BadRequest(commandResult.ValidationResult);
            }
            await _unitOfWork.CommitAsync();
            return Ok();
        }
    }
}
