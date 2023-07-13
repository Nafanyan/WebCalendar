using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Presentation.Intranet.Api
{
    public class TokenValidationAttribute : Attribute
    {
        public long UserId { get; }
        public bool IsValid { get; }

    }
}
