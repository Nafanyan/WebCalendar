namespace Application.Result
{
    public class ResultQuery<T> where T : class
    {
        public T ResultObj { get; private set; }
        public string MSG { get; private set; }

        public ResultQuery(T result, string msg)
        {
            ResultObj = result;
            MSG = msg;
        }
        public ResultQuery(string msg)
        {
            MSG = msg;
        }
    }
}
