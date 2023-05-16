namespace Application.Validation
{
    public interface IValidator<T>
    {
        ValidationResult Validation(T inputData);
    }
}
