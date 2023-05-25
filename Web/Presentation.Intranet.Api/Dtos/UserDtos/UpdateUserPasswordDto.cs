namespace Presentation.Intranet.Api.Dtos.UserDtos
{
    public class UpdateUserPasswordDto
    {
        public long Id { get; init; }
        public string OldPasswordHash { get; init; }
        public string NewPasswordHash { get; init; }
    }
}
