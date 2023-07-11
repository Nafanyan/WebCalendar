using Application.Interfaces;
using Application.Result;
using Application.Validation;
using Domain.Entities;
using Domain.Repositories;
using Domain.UnitOfWork;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Application.UserAuthorizationTokens.Commands.UserAuthorization
{
    public class AuthorizationUserQueryHandler : ICommandHandler<UserAuthorizationCommand>
    {
        private readonly UserAuthorizationTokenRepository _userAuthorizationTokenRepository;
        private readonly IAsyncValidator<UserAuthorizationCommand> _userAuthorizationValidator;
        private readonly IUnitOfWork _unitOfWork;

        private string _secret = "MyVerySecretKey41 25345 4647 sd afs fdsg s agf";
        private int _tokenValidityInMinutes = 15;
        private int _refreshTokenValidityInDays = 15;

        public AuthorizationUserQueryHandler(
            UserAuthorizationTokenRepository userAuthorizationTokenRepository,
            IAsyncValidator<UserAuthorizationCommand> validator,
            IUnitOfWork unitOfWork
            )
        {
            _userAuthorizationTokenRepository = userAuthorizationTokenRepository;
            _userAuthorizationValidator = validator;
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandResult> HandleAsync(UserAuthorizationCommand command)
        {
            //List<Claim> authClaims = new List<Claim>
            //{
            //    new Claim(nameof(query.UserId), query.UserId.ToString())
            //};

            if (! await _userAuthorizationTokenRepository.ContainsAsync(token => token.UserId == command.UserId))
            {
                await _userAuthorizationTokenRepository.AddAsync(new UserAuthorizationToken(command.RefreshToken, command.UserId));
                await _unitOfWork.CommitAsync();
                return new CommandResult(ValidationResult.Ok());
            }

            if (command.RefreshToken == (await _userAuthorizationTokenRepository.GetTokenAsync(command.UserId)).RefreshToken)
            {
                return new CommandResult(ValidationResult.Ok());
            }

            ValidationResult validationResult = await _userAuthorizationValidator.ValidationAsync(command);
            if (validationResult.IsFail)
            {
                return new CommandResult(validationResult);
            }


            //string newAccessToken = new JwtSecurityTokenHandler().WriteToken(CreateToken(authClaims));
            //string newRefreshToken = GenerateRefreshToken();

            //if (await _userAuthorizationRepository.ContainsAsync(token => token.UserId == query.UserId))
            //{
            //    UserAuthorizationToken token = await _userAuthorizationRepository.GetTokenAsync(query.UserId);
            //    token.SetRefreshToken(newRefreshToken);
            //}
            //else
            //{
            //    await _userAuthorizationRepository.AddAsync(new UserAuthorizationToken(newRefreshToken, query.UserId));
            //}
            //await _unitOfWork.CommitAsync();

            //return new QueryResult<GetTokenQueryDto>(new GetTokenQueryDto
            //{
            //    AccessToken = newAccessToken,
            //    RefreshToken = newRefreshToken
            //});
            return new CommandResult(validationResult);
        }
    }
}
