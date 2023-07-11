
namespace Domain.Entities
{
    public class UserAuthorizationToken
    {
        public string RefreshToken { get; private set; }
        public long UserId { get; init; }

        public UserAuthorizationToken (string refreshToken, long userId)
        {
            RefreshToken = refreshToken;
            UserId = userId;
        }

        public void SetRefreshToken(string refreshToken)
        {
            RefreshToken = refreshToken;
        }
    }
}
