﻿using Application.Interfaces;
using Application.Result;
using Application.UserAuthorizationTokens.DTOs;
using Application.Validation;
using Domain.Entities;
using Domain.Repositories;
using Domain.UnitOfWork;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Application.UserAuthorizationTokens
{
    public class AuthorizationUserQueryHandler : IQueryHandler<GetTokenQueryDto, UserAuthorizationQuery>
    {
        private readonly UserAuthorizationTokenRepository _userAuthorizationRepository;
        private readonly IAsyncValidator<UserAuthorizationQuery> _userAuthorizationValidator;
        private readonly IUnitOfWork _unitOfWork;

        private string _secret = "MyVerySecretKey41 25345 4647 sd afs fdsg s agf";
        private int _tokenValidityInMinutes = 15;
        private int _refreshTokenValidityInDays = 15;

        public AuthorizationUserQueryHandler(
            UserAuthorizationTokenRepository userAuthorizationRepository,
            IAsyncValidator<UserAuthorizationQuery> validator,
            IUnitOfWork unitOfWork
            )
        {
            _userAuthorizationRepository = userAuthorizationRepository;
            _userAuthorizationValidator = validator;
            _unitOfWork = unitOfWork;
        }

        public async Task<QueryResult<GetTokenQueryDto>> HandleAsync(UserAuthorizationQuery query)
        {
            //List<Claim> authClaims = new List<Claim>
            //{
            //    new Claim(nameof(query.UserId), query.UserId.ToString())
            //};

            if (query.RefreshToken == (await _userAuthorizationRepository.GetTokenAsync(query.UserId)).RefreshToken)
            {
                //string newAccessToken = new JwtSecurityTokenHandler().WriteToken(CreateToken(authClaims));
                //return new QueryResult<GetTokenQueryDto>(new GetTokenQueryDto
                //{
                //    AccessToken = newAccessToken,
                //    RefreshToken = query.RefreshToken
                //});
            }

            ValidationResult validationResult = await _userAuthorizationValidator.ValidationAsync(query);
            if (!validationResult.IsFail)
            {
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
            }

            return new QueryResult<GetTokenQueryDto>(validationResult);
        }
    }
}
