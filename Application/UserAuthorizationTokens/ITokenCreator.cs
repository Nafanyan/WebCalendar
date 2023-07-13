namespace Application.UserAuthorizationTokens
{
    public interface ITokenCreator
    {
        string CreateAccessToken(long userId);
        string CreateRefreshToken();
    }
}
