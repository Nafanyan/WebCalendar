using System.IdentityModel.Tokens.Jwt;

namespace Presentation.Intranet.Api.ActionOnToken.DecodeToken
{
    public class TokenDecoder
    {
        public JwtSecurityToken DecodeToken(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(accessToken);
            JwtSecurityToken tokenS = jsonToken as JwtSecurityToken;
            return tokenS;
        }
    }
}
