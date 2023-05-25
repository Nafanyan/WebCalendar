namespace Presentation.Intranet.Api.Dtos.UserDtos
{
    public class DeleteUserDto
    {
        public long Id { get; init; }
        public string PasswordHash { get; init; }
    }
}
