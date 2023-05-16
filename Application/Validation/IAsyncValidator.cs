namespace Application.Validation
{
    public interface IAsyncValidator<T> where T : class
    {
        Task<ValidationResult> AsyncValidation(T inputData);
    }
}
