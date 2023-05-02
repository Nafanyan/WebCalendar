namespace WebCalendar.Domain.Validation.EventValidation
{
    internal class EventException : Exception
    {
        public EventException(string message) : base(message)
        {
        }
    }
}
