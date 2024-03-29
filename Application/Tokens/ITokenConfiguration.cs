﻿namespace Application.Tokens
{
    public interface ITokenConfiguration
    {
        string GetSecret();
        int GetAccessTokenValidityInMinutes();
        int GetRefreshTokenValidityInDays();
    }
}
