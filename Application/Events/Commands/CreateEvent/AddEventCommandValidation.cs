namespace Application.Events.Commands.CreateEvent
{
    class AddEventCommandValidation : IValidation<AddEventCommand>
    {
        public string Validation(AddEventCommand command)
        {
            if (command.Name == null)
            {
                return "The name of event cannot be empty/cannot be null";
            }

            if (command.StartEvent == null)
            {
                return "The start date cannot be empty/cannot be null";
            }

            if (command.StartEvent == null)
            {
                return "The end date cannot be empty/cannot be null";
            }

            if (command.StartEvent > command.EndEvent)
            {
                return "The start date cannot be later than the end date";
            }

            return "Ok";
        }
    }
}
