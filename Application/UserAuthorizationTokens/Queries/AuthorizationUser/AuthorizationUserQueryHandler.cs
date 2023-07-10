using Application.Interfaces;
using Application.Result;
using Application.UserAuthorizationTokens.DTOs;
using Application.Validation;
using Domain.Entities;
using Domain.Repositories;
using Domain.UnitOfWork;

namespace Application.UserAuthorizationTokens.Queries.AuthorizationUser
{
    public class AuthorizationUserQueryHandler : IQueryHandler<GetTokenQueryDto, AuthorizationUserQuery>
    {
        private readonly IUserAuthorizationRepository _userAuthorizationRepository;
        private readonly IAsyncValidator<AuthorizationUserQuery> _userAuthorizationValidator;
        private readonly IUnitOfWork _unitOfWork;

        public AuthorizationUserQueryHandler(
            IUserAuthorizationRepository userAuthorizationRepository,
            IAsyncValidator<AuthorizationUserQuery> validator,
            IUnitOfWork unitOfWork
            )
        {
            _userAuthorizationRepository = userAuthorizationRepository;
            _userAuthorizationValidator = validator;
            _unitOfWork = unitOfWork;
        }
        public async Task<QueryResult<GetTokenQueryDto>> HandleAsync(AuthorizationUserQuery query)
        {
            ValidationResult validationResult = await _userAuthorizationValidator.ValidationAsync(query);

            if (! validationResult.IsFail)
            {
                UserAuthorizationToken token = await _userAuthorizationRepository.GetTokenAsync(query.UserId);
                // меняем значения токенов
                token.SetAccessToken("");
                token.SetRefreshToken("");
                
                GetTokenQueryDto getTokenQueryDto = new GetTokenQueryDto
                {
                    AccessToken = token.AccessToken,
                    RefreshToken = token.RefreshToken
                };
                await _unitOfWork.CommitAsync();
                return new QueryResult<GetTokenQueryDto>(getTokenQueryDto);
            }

            return new QueryResult<GetTokenQueryDto>(validationResult);
        }
    }
}
