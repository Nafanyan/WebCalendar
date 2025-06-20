﻿using Application.Interfaces;
using Application.Result;
using Application.Users.DTOs;
using Application.Validation;
using Domain.Entities;
using Application.Repositories;

namespace Application.Users.Queries.GetEvents
{
    public class GetEventsQueryHandler : IQueryHandler<IReadOnlyList<GetEventsQueryDto>, GetEventsQuery>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAsyncValidator<GetEventsQuery> _getEventsQueryValidator;

        public GetEventsQueryHandler( IUserRepository userRepository, IAsyncValidator<GetEventsQuery> validator )
        {
            _userRepository = userRepository;
            _getEventsQueryValidator = validator;
        }

        public async Task<QueryResult<IReadOnlyList<GetEventsQueryDto>>> HandleAsync( GetEventsQuery getEventsQuery )
        {
            ValidationResult validationResult = await _getEventsQueryValidator.ValidationAsync( getEventsQuery );
            if( validationResult.IsFail )
            {
                return new QueryResult<IReadOnlyList<GetEventsQueryDto>>( validationResult );
            }

            IReadOnlyList<Event> events = await _userRepository.GetEventsAsync( getEventsQuery.UserId,
                getEventsQuery.StartEvent, getEventsQuery.EndEvent );

            IReadOnlyList<Event> sortEvents = events.OrderBy( e => e.StartEvent ).ToList();

            List<GetEventsQueryDto> getEventsQueryDtos = sortEvents.Select( e => new GetEventsQueryDto
            {
                UserId = e.UserId,
                Name = e.Name,
                Description = e.Description,
                StartEvent = e.StartEvent,
                EndEvent = e.EndEvent
            } ).ToList();
            return new QueryResult<IReadOnlyList<GetEventsQueryDto>>( getEventsQueryDtos );
        }
    }
}
