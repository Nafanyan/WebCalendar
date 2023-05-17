namespace Application.Validation
{
    public class ValidationResult
    {
        public bool IsFail { get; private set; } = false;
        public string Error { get; private set; }

        public ValidationResult(string error = null)
        {
            Error = error;
            if (error != null)
            {
                IsFail = true;
            }
            
        }

        public static ValidationResult Ok()
        {
            return new ValidationResult();
        }
        public static ValidationResult Fail(string error)
        {
            return new ValidationResult(error);
        }
    }
}
