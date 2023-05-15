using Domain.Repositories;
using System.Xml.Linq;

namespace Application.Events.Commands.CreateEvent
{
    class AddEventCommandValidation : IValidation<AddEventCommand>
    {
        public string Validation(AddEventCommand addEventCommand)
        {
            if (addEventCommand.Name == null)
            {
                return "The name of event cannot be empty/cannot be null";
            }

            if (addEventCommand.StartEvent == null)
            {
                return "The start date cannot be empty/cannot be null";
            }

            if (addEventCommand.StartEvent == null)
            {
                return "The end date cannot be empty/cannot be null";
            }

            if (addEventCommand.StartEvent > addEventCommand.EndEvent)
            {
                return "The start date cannot be later than the end date";
            }

            return "Ok";
        }
    }
}
