
namespace Domain.Entities
{
    public class JWT
    {
        public string AccessToken { get; private set; }
        public string RefreshToken { get; private set; }
        public long UserId { get; init; }

        public JWT (string accessToken, string refreshToken, long userId)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            UserId = userId;
        }

        public void setAccessToken(string accessToken)
        {
            AccessToken = accessToken;
        }
        public void setRefreshToken(string refreshToken)
        {
            RefreshToken = refreshToken;
        }
    }
}
