namespace Application.Validation
{
    public class ValidationResult
    {
        public bool IsFail { get; private set; }
        public string Error { get; private set; }

        public ValidationResult(bool isFail, string error)
        {
            IsFail = isFail;
            Error = error;
        }
    }
}
