
namespace Domain.Entities
{
    public class UserAuthorizationToken
    {
        public string RefreshToken { get; init; }
        public long UserId { get; init; }
        public DateTime ExpiryDate { get; init; }
        public UserAuthorizationToken (string refreshToken, long userId, DateTime expiryDate)
        {
            RefreshToken = refreshToken;
            UserId = userId;
            ExpiryDate = expiryDate;
        }
    }
}
