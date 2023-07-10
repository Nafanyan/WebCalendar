using Application.Events.DTOs;
using Application.Interfaces;
using Application.JWTs.DTOs;
using Application.Result;
using Application.Validation;
using Domain.Entities;
using Domain.Repositories;
using Domain.UnitOfWork;

namespace Application.JWTs.Queries.GetJWT
{
    public class GetJWTQueryHandler : IQueryHandler<GetJWTQueryDto, GetJWTQuery>
    {
        private readonly IJWTRepository _jWTRepository;
        private readonly IAsyncValidator<GetJWTQuery> _getJWTQueryValidator;
        private readonly IUnitOfWork _unitOfWork;

        public GetJWTQueryHandler(
            IJWTRepository jWTRepository,
            IAsyncValidator<GetJWTQuery> validator,
            IUnitOfWork unitOfWork)
        {
            _jWTRepository = jWTRepository;
            _getJWTQueryValidator = validator;
            _unitOfWork = unitOfWork;
        }
        public async Task<QueryResult<GetJWTQueryDto>> HandleAsync(GetJWTQuery query)
        {
            ValidationResult validationResult = await _getJWTQueryValidator.ValidationAsync(query);

            if(!validationResult.IsFail)
            {
                // create JWT token ...

                JWT jwt = await _jWTRepository.GetJWTAsync(query.UserId);

                // update JWT token
                jwt.setAccessToken("");
                jwt.setRefreshToken("");
                await _unitOfWork.CommitAsync();

                GetJWTQueryDto getJWTQueryDto = new GetJWTQueryDto
                {
                    AccessToken = jwt.AccessToken,
                    RefreshToken = jwt.RefreshToken
                };

                return new QueryResult<GetJWTQueryDto>(getJWTQueryDto);
            }

            return new QueryResult<GetJWTQueryDto>(validationResult);
        }
    }
}
