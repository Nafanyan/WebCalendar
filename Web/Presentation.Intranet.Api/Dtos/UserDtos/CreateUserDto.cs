namespace Presentation.Intranet.Api.Dtos.UserDtos
{
    public class CreateUserDto
    {
        public string Login { get; init; }
        public string PasswordHash { get; init; }
    }
}
