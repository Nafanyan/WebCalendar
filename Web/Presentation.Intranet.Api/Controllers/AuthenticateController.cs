using Application.Interfaces;
using Application.UserAuthorizationTokens.DTOs;
using Application.UserAuthorizationTokens;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Intranet.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticateController : ControllerBase
    {
        private readonly IQueryHandler<GetTokenQueryDto, AuthorizationUserQuery> _authorizationUserQueryHandler;

        public AuthenticateController(IQueryHandler<GetTokenQueryDto, AuthorizationUserQuery> authorizationUserQueryHandler)
        {
            _authorizationUserQueryHandler = authorizationUserQueryHandler;
        }


        [HttpGet()]
        public async Task<IActionResult> CheckToken()
        {
            AuthorizationUserQuery query = new AuthorizationUserQuery
            {
                UserId = 4,
                Login = "s",
                PasswordHash = "s"
            };

            return Ok(_authorizationUserQueryHandler.HandleAsync(query));
        }
    }
}
