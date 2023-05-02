namespace WebCalendar.Domain.Events
{
    public interface IEventRepository
    {
        Event GetById (long id);
        void Add (Event e);
        void Update (Event e);
        void Delete (long id);
    }
}
