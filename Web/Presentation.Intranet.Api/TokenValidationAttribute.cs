using Microsoft.AspNetCore.Mvc;
using Presentation.Intranet.Api.ActionOnToken.DecodeToken;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using ValidationResult = Application.Validation.ValidationResult;

namespace Presentation.Intranet.Api
{
    public class TokenValidationAttribute : Attribute
    {
        public ValidationResult TokenValidationResult { get; private set; }

        public bool TokenIsValid(ControllerBase controller)
        {
            string accessToken = controller.Request.Headers["Access-token"];
            IConfiguration configuration = controller.HttpContext.RequestServices.GetService<IConfiguration>();

            if (!VerifySignature(accessToken, configuration["JWT:Secret"]))
            {
                return false;
            }

            TokenDecoder tokenDecoder = new TokenDecoder();
            JwtSecurityToken token = tokenDecoder.DecodeToken(accessToken);

            if (token.ValidTo < DateTime.Now)
            {
                return false;
            }

            long requestId = (long)Convert.ToDouble(controller.Request.RouteValues["userId"]);
            long tokenId = (long)Convert.ToDouble(token.Payload["userId"]);

            if (requestId != tokenId)
            {
                return false;
            }

            return true;
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
            output = output.Split('=')[0]; // Remove any trailing '='s
            output = output.Replace('+', '-'); // 62nd char of encoding
            output = output.Replace('/', '_'); // 63rd char of encoding
            return output;
        }
    }
}
