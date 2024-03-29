﻿using Application.Entities;
using Application.Events.Queries.GetEvent;
using Application.Repositories;
using Application.Validation;
using Infrastructure.Entities.Events;
using Infrastructure.Entities.Users;
using Infrastructure.Foundation;
using Microsoft.EntityFrameworkCore;

namespace Application.Tests.Events.Queries.GetEvent
{
    public class GetEventQueryValidatorTests
    {
        private IAsyncValidator<GetEventQuery> _validator;

        [SetUp]
        public async Task Setup()
        {
            string dbName = $"EventDb_{DateTime.Now.ToFileTimeUtc()}";
            DbContextOptions<WebCalendarDbContext> dbContextOptions = new DbContextOptionsBuilder<WebCalendarDbContext>()
                .UseInMemoryDatabase( dbName )
                .Options;
            WebCalendarDbContext webCalendarDbContext = new WebCalendarDbContext( dbContextOptions );

            IEventRepository eventRepository = new EventRepository( webCalendarDbContext );
            await InitData( eventRepository, webCalendarDbContext );

            _validator = new GetEventQueryValidator( eventRepository );
        }

        [Test]
        public async Task ValidationAsync_StartDateAndEndDateNotSameDay_Fail()
        {
            // arrange
            GetEventQuery getEventQuery = new GetEventQuery
            {
                UserId = 0,
                StartEvent = new DateTime( 2023, 11, 1 ),
                EndEvent = new DateTime( 2023, 11, 2 )
            };

            // act
            ValidationResult result = await _validator.ValidationAsync( getEventQuery );

            // assert
            Assert.IsTrue( result.IsFail );
        }

        [Test]
        public async Task ValidationAsync_StartDateLaterThenEndDate_Fail()
        {
            // arrange
            GetEventQuery getEventQuery = new GetEventQuery
            {
                UserId = 0,
                StartEvent = new DateTime( 1001 ),
                EndEvent = new DateTime( 1000 )
            };

            // act
            ValidationResult result = await _validator.ValidationAsync( getEventQuery );

            // assert
            Assert.IsTrue( result.IsFail );
        }

        [Test]
        public async Task ValidationAsync_StartDateIncludedInExistDateEvent_Success()
        {
            // arrange
            GetEventQuery getEventQuery = new GetEventQuery
            {
                UserId = 1,
                StartEvent = new DateTime( 1500 ),
                EndEvent = new DateTime( 2500 )
            };

            // act
            ValidationResult result = await _validator.ValidationAsync( getEventQuery );

            // assert
            Assert.IsFalse( result.IsFail );
        }

        [Test]
        public async Task ValidationAsync_EndDateIncludedInExistDateEvent_Success()
        {
            // arrange
            GetEventQuery getEventQuery = new GetEventQuery
            {
                UserId = 1,
                StartEvent = new DateTime( 500 ),
                EndEvent = new DateTime( 1500 )
            };

            // act
            ValidationResult result = await _validator.ValidationAsync( getEventQuery );

            // assert
            Assert.IsFalse( result.IsFail );
        }

        [Test]
        public async Task ValidationAsync_InputDateEventIncludedExistDateEvent_Success()
        {
            // arrange
            GetEventQuery getEventQuery = new GetEventQuery
            {
                UserId = 1,
                StartEvent = new DateTime( 100 ),
                EndEvent = new DateTime( 2500 )
            };

            // act
            ValidationResult result = await _validator.ValidationAsync( getEventQuery );

            // assert
            Assert.IsFalse( result.IsFail );
        }

        [Test]
        public async Task ValidationAsync_InpitDateEventNotIncludedExistDateEvents_Fail()
        {
            // arrange
            GetEventQuery getEventQuery = new GetEventQuery
            {
                UserId = 1,
                StartEvent = new DateTime( 100 ),
                EndEvent = new DateTime( 500 )
            };

            // act
            ValidationResult result = await _validator.ValidationAsync( getEventQuery );

            // assert
            Assert.IsTrue( result.IsFail );
        }

        private async Task InitData( IEventRepository eventRepository, WebCalendarDbContext webCalendarDbContext )
        {
            IUserRepository userRepository = new UserRepository( webCalendarDbContext );
            User user = new User( "login", "passwordHash" );
            userRepository.Add( user );

            Event newEvent = new Event( 1, "name", "", new DateTime( 1000 ), new DateTime( 2000 ) );
            eventRepository.Add( newEvent );

            IUnitOfWork unitOfWork = new UnitOfWork( webCalendarDbContext );
            await unitOfWork.CommitAsync();
        }
    }
}
