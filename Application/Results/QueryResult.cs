﻿using Application.Validation;

namespace Application.Result
{
    public class QueryResult<TQueryResultData> where TQueryResultData : class
    {
        public ValidationResult ValidationResult { get; private set; }
        public TQueryResultData ObjResult { get; private set; }

        public QueryResult(ValidationResult validationResult, TQueryResultData objResult)
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
