using Application.Result;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Events.Commands.CreateEvent
{
    public class AddEventCommandHandler : IEventCommandHandler<AddEventCommand>
    {
        private readonly IEventRepository _eventRepository;
        private readonly AddEventCommandValidation _addEventCommandValidation;

        public AddEventCommandHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
            _addEventCommandValidation = new AddEventCommandValidation();
        }

        public async Task<ResultCommand> Handle(AddEventCommand addEventCommand)
        {
            string msg = _addEventCommandValidation.Validation(addEventCommand);
            if (msg == "Ok")
            {
                EventPeriod eventPeriod = new EventPeriod(addEventCommand.StartEvent, addEventCommand.EndEvent);
                Event newEvent = new Event(addEventCommand.Name, addEventCommand.Description, eventPeriod);
                await _eventRepository.Add(newEvent);
            }
            return new ResultCommand(msg);
        }

    }
}
