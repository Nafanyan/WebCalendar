namespace Application.Validation
{
    public interface IValidation<T>
    {
        ValidationResult Validation(T inputData);
    }
}
