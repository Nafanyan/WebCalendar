
namespace Presentation.Intranet.Api
{
    public class TokenValidationAttribute : Attribute
    {
        public long UserId { get; }
        public bool IsValid { get; }
    }
}
