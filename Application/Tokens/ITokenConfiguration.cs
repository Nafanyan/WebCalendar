namespace Application.Tokens
{
    public interface ITokenConfiguration
    {
        string GetSecret();
        string GetAccessTokenValidityInMinutes();
        string GetRefreshTokenValidityInDays();
    }
}
