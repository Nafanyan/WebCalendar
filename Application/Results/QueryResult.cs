using Application.Validation;

namespace Application.Result
{
    public class QueryResult<T> where T : class
    {
        public ValidationResult ValidationResult { get; private set; }
        public T ObjResult { get; private set; }

        public QueryResult(ValidationResult validationResult, T objResult)
        {
            ValidationResult = validationResult;
            ObjResult = objResult;
        }

        public QueryResult(ValidationResult validationResult)
        {
            ValidationResult = validationResult;
        }
    }
}
