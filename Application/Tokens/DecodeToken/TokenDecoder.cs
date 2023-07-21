﻿using System.IdentityModel.Tokens.Jwt;

namespace Application.Tokens.DecodeToken
{
    public class TokenDecoder
    {
        public JwtSecurityToken DecodeToken(string accessToken)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.ReadToken(accessToken) as JwtSecurityToken;
            return token;
        }
    }
}
