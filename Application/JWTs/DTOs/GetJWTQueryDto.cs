namespace Application.JWTs.DTOs
{
    public class GetJWTQueryDto
    {
        public string AccessToken { get; init; }
        public string RefreshToken { get; init; }
    }
}
