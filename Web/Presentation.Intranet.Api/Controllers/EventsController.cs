using Application.Events.Commands.CreateEvent;
using Application.Events.Commands.DeleteEvent;
using Application.Events.Commands.UpdateEvent;
using Application.Events.Queries.GetEvent;
using Application.Interfaces;
using Application.Result;
using Domain.Entities;
using Domain.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Presentation.Intranet.Api.Dtos.EventDtos;
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
        public async Task<IActionResult> CreateEvent([FromRoute] long userId, [FromBody] CreateEventDto createEventDto)
        {
            if (createEventDto.StartEvent.StringValidationToDate().IsFail)
            {
                return BadRequest(createEventDto.StartEvent.StringValidationToDate().Error);
            }
            if (createEventDto.EndEvent.StringValidationToDate().IsFail)
            {
                return BadRequest(createEventDto.EndEvent.StringValidationToDate().Error);
            }

            CommandResult commandResult = await _createEventCommandHandler.HandleAsync(createEventDto.Map(userId));

            if (commandResult.ValidationResult.IsFail)
            {
                return BadRequest(commandResult.ValidationResult);
            }

            await _unitOfWork.CommitAsync();
            return Ok();
        }

        [HttpDelete("{userId:long}/[controller]")]
        public async Task<IActionResult> DeleteEvent([FromRoute]long userId, [FromBody] DeleteEventDto deleteEventDto)
        {
            if (deleteEventDto.StartEvent.StringValidationToDate().IsFail)
            {
                return BadRequest(deleteEventDto.StartEvent.StringValidationToDate().Error);
            }
            if (deleteEventDto.EndEvent.StringValidationToDate().IsFail)
            {
                return BadRequest(deleteEventDto.EndEvent.StringValidationToDate().Error);
            }

            CommandResult commandResult = await _deleteEventCommandHandler.HandleAsync(deleteEventDto.Map(userId));

            if (commandResult.ValidationResult.IsFail)
            {
                return BadRequest(commandResult.ValidationResult);
            }

            await _unitOfWork.CommitAsync();
            return Ok();
        }

        [HttpPut("{userId:long}/[controller]")]
        public async Task<IActionResult> UpdateEvent([FromRoute] long userId, [FromBody] UpdateEventDto updateEventDto)
        {
            if (updateEventDto.StartEvent.StringValidationToDate().IsFail)
            {
                return BadRequest(updateEventDto.StartEvent.StringValidationToDate().Error);
            }
            if (updateEventDto.EndEvent.StringValidationToDate().IsFail)
            {
                return BadRequest(updateEventDto.EndEvent.StringValidationToDate().Error);
            }

            CommandResult commandResult = await _updateEventCommandHandler.HandleAsync(updateEventDto.Map(userId));

            if (commandResult.ValidationResult.IsFail)
            {
                return BadRequest(commandResult.ValidationResult);
            }
            await _unitOfWork.CommitAsync();
            return Ok();
        }
    }
}
