namespace Application.Result
{
    public class ResultCommand
    {
        public string MSG { get; private set; }

        public ResultCommand(string msg)
        {
            MSG = msg;
        }
    }
}
