namespace Presentation.Intranet.Api.Dtos.AuthenticationDtos
{
    public class AuthenticationWithLoginDto
    {
        public long UserId { get; init; }
        public string Login { get; init; }
        public string PasswordHash { get; init; }
    }
}
