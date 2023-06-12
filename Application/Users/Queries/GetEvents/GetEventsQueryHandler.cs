using Application.Interfaces;
using Application.Result;
using Application.Users.DTOs;
using Application.Validation;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Users.Queries.GetEvents
{
    public class GetEventsQueryHandler : IQueryHandler<GetEventsQueryDto, GetEventsQuery>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAsyncValidator<GetEventsQuery> _getEventsQueryValidator;

        public GetEventsQueryHandler(IUserRepository userRepository, IAsyncValidator<GetEventsQuery> validator)
        {
            _userRepository = userRepository;
            _getEventsQueryValidator = validator;
        }

        public async Task<QueryResult<GetEventsQueryDto>> HandleAsync(GetEventsQuery getEventsQuery)
        {
            ValidationResult validationResult = await _getEventsQueryValidator.ValidationAsync(getEventsQuery);
            if (!validationResult.IsFail)
            {
                IReadOnlyList<Event> events = await _userRepository.GetEventsAsync(getEventsQuery.UserId);
                GetEventsQueryDto getEventsQueryDto = new GetEventsQueryDto
                {
                    events = events.ToList()
                };
                return new QueryResult<GetEventsQueryDto>(getEventsQueryDto);
            }
            return new QueryResult<GetEventsQueryDto>(validationResult);
        }
    }
}
