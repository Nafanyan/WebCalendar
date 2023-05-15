namespace Application
{
    public interface IValidation<T>
    {
        string Validation(T inputData);
    }
}
