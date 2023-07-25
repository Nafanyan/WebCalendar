namespace Presentation.Intranet.Api.Dtos.UserDtos
{
    public class UpdateUserPasswordDto
    {
        public string OldPasswordHash { get; init; }
        public string NewPasswordHash { get; init; }
    }
}
