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

        [HttpGet("{userId}/[controller]/{startEvent}/{endEvent}")]
        public async Task<IActionResult> GetEvent([FromRoute] long userId, string startEvent, string endEvent)
        {
            GetEventQuery getEventQuery = new GetEventQuery
            {
                UserId = userId,
                StartEvent = startEvent.MapStringToDate(),
                EndEvent = endEvent.MapStringToDate()
            };
            QueryResult<Event> queryResult = await _getEventQueryHandler.HandleAsync(getEventQuery);

            if(queryResult.ValidationResult.IsFail)
            {
                return BadRequest(queryResult.ValidationResult);
            }
            return Ok(queryResult.ObjResult);
        }

        [HttpPost("{userId}/[controller]")]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventDto createEventDto)
        {
            CommandResult commandResult = await _createEventCommandHandler.HandleAsync(createEventDto.Map());

            if(commandResult.ValidationResult.IsFail)
            {
                return BadRequest(commandResult.ValidationResult);
            }
            await _unitOfWork.CommitAsync();
            return Ok();
        }

        [HttpDelete("{userId}/[controller]")]
        public async Task<IActionResult> DeleteEvent([FromBody] DeleteEventDto deleteEventDto)
        {
            CommandResult commandResult = await _deleteEventCommandHandler.HandleAsync(deleteEventDto.Map());

            if (commandResult.ValidationResult.IsFail)
            {
                return BadRequest(commandResult.ValidationResult);
            }
            await _unitOfWork.CommitAsync();
            return Ok();
        }

        [HttpPut("{userId}/[controller]")]
        public async Task<IActionResult> UpdateEvent([FromBody] UpdateEventDto updateEventDto)
        {
            CommandResult commandResult = await _updateEventCommandHandler.HandleAsync(updateEventDto.Map());

            if(commandResult.ValidationResult.IsFail)
            {
                return BadRequest(commandResult.ValidationResult);
            }
            await _unitOfWork.CommitAsync();
            return Ok();
        }
    }
}
