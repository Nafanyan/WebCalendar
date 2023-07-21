using Application.Tokens;
using Application.Tokens.DecodeToken;
using Application.Tokens.VerificationToken;
using Infrastructure.ConfigurationUtils.Token;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;
using ValidationResult = Application.Validation.ValidationResult;

namespace Infrastructure.JwtAuthorizations
{
    public class JwtAuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        public ValidationResult TokenValidationResult { get; private set; }
        private readonly ITokenConfiguration _configuration;

        public JwtAuthorizationAttribute()
        {
            _configuration = new TokenConfiguration();
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string accessToken = context.HttpContext.Request.Headers["Access-Token"];

            if (accessToken is null)
            {
                context.Result = new ForbidResult();
            }

            TokenSignatureVerifycator verifySignatureToken = new TokenSignatureVerifycator(accessToken, _configuration.GetSecret());
            if (!verifySignatureToken.TokenIsValid)
            {
                context.Result = new ForbidResult();
            }

            TokenDecoder tokenDecoder = new TokenDecoder();
            JwtSecurityToken token = tokenDecoder.DecodeToken(accessToken);
            DateTime expDate = new DateTime(1970, 1, 1).AddSeconds((token.Payload.Exp.Value)).AddHours(3);

            if (DateTime.Now > expDate)
            {
                context.Result = new ForbidResult();
            }

            long requestId = (long)Convert.ToDouble(context.HttpContext.Request.RouteValues["userId"]);
            long tokenId = (long)Convert.ToDouble(token.Payload["userId"]);
            if (requestId != tokenId)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
