namespace Application.Result
{
    public class Result<T> where T : class
    {
        public T ResultObj { get; private set; }
        public string MSG { get; private set; }

        public Result(T result, string msg)
        {
            ResultObj = result;
            MSG = msg;
        }
        public Result(string msg)
        {
            MSG = msg;
        }
    }
}
