
namespace Domain.Entities
{
    public class UserAuthorizationToken
    {
        public string AccessToken { get; private set; }
        public string RefreshToken { get; private set; }
        public long UserId { get; init; }

        public UserAuthorizationToken (string accessToken, string refreshToken, long userId)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            UserId = userId;
        }

        public void SetAccessToken(string accessToken)
        {
            AccessToken = accessToken;
        }
        public void SetRefreshToken(string refreshToken)
        {
            RefreshToken = refreshToken;
        }
    }
}
