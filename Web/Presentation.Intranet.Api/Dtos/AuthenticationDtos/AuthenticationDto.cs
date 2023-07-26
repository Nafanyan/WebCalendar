namespace Presentation.Intranet.Api.Dtos.AuthenticationDtos
{
    public class AuthenticationDto
    {
        public string Login { get; init; }
        public string PasswordHash { get; init; }
    }
}