using Application.Tokens.DecodeToken;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using ValidationResult = Application.Validation.ValidationResult;

namespace Presentation.Intranet.Api
{
    public class JwtAuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        public ValidationResult TokenValidationResult { get; private set; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string accessToken = context.HttpContext.Request.Headers["Access-Token"];
            IConfiguration configuration = context.HttpContext.RequestServices.GetService<IConfiguration>();

            if (accessToken is null)
            {
                context.Result = new ForbidResult();
            }

            accessToken = accessToken.Replace("\"", "");
            if (!VerifySignature(accessToken, configuration["JWT:Secret"]))
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

        private bool VerifySignature(string accessToken, string secret)
        {
            string[] parts = accessToken.Split(".".ToCharArray());
            string header = parts[0];
            string payload = parts[1];
            string signature = parts[2];

            byte[] bytesToSign = Encoding.UTF8.GetBytes(string.Join(".", header, payload));
            byte[] bytesToSecret = Encoding.UTF8.GetBytes(secret);

            HMACSHA256 alg = new HMACSHA256(bytesToSecret);
            byte[] hash = alg.ComputeHash(bytesToSign);

            string computedSignature = Base64UrlEncode(hash);

            return signature == computedSignature;
        }

        private string Base64UrlEncode(byte[] input)
        {
            var output = Convert.ToBase64String(input);
            output = output.Split('=')[0]; 
            output = output.Replace('+', '-');
            output = output.Replace('/', '_'); 
            return output;
        }
    }
}
