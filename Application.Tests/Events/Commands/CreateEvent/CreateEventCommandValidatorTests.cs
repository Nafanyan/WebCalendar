using Application.Entities;
using Application.Events.Commands.CreateEvent;
using Application.Repositories;
using Application.Validation;
using Infrastructure.Entities.Events;
using Infrastructure.Entities.Users;
using Infrastructure.Foundation;
using Microsoft.EntityFrameworkCore;

namespace Application.Tests.Events.Commands.CreateEvent
{
    public class CreateEventCommandValidatorTests
    {
        private IAsyncValidator<CreateEventCommand> _validator;

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

            _validator = new CreateEventCommandValidator( eventRepository );
        }

        [Test]
        public async Task ValidationAsync_NameIsNull_Fail()
        {
            // arrange
            CreateEventCommand commcreateEventCommandnd = new CreateEventCommand
            {
                Name = null
            };

            // act
            ValidationResult result = await _validator.ValidationAsync( commcreateEventCommandnd );

            // assert
            Assert.IsTrue( result.IsFail );
        }

        [Test]
        public async Task ValidationAsync_NameIsEmpty_Fail()
        {
            // arrange
            CreateEventCommand createEventCommand = new CreateEventCommand
            {
                Name = ""
            };

            // act
            ValidationResult result = await _validator.ValidationAsync( createEventCommand );

            // assert
            Assert.IsTrue( result.IsFail );
        }

        [Test]
        public async Task ValidationAsync_StartDateAndEndDateNotSameDay_Fail()
        {
            // arrange
            CreateEventCommand createEventCommand = new CreateEventCommand
            {
                Name = "name",
                StartEvent = new DateTime( 2023, 11, 1 ),
                EndEvent = new DateTime( 2023, 11, 2 )
            };

            // act
            ValidationResult result = await _validator.ValidationAsync( createEventCommand );

            // assert
            Assert.IsTrue( result.IsFail );
        }

        [Test]
        public async Task ValidationAsync_StartDateLaterThenEndDate_Fail()
        {
            // arrange
            CreateEventCommand createEventCommand = new CreateEventCommand
            {
                Name = "name",
                StartEvent = new DateTime( 1001 ),
                EndEvent = new DateTime( 1000 )
            };

            // act
            ValidationResult result = await _validator.ValidationAsync( createEventCommand );

            // assert
            Assert.IsTrue( result.IsFail );
        }

        [Test]
        public async Task ValidationAsync_StartDateIncludedInExistDateEvent_Fail()
        {
            // arrange
            CreateEventCommand createEventCommand = new CreateEventCommand
            {
                UserId = 1,
                Name = "name",
                Description = "",
                StartEvent = new DateTime( 1500 ),
                EndEvent = new DateTime( 2500 )
            };

            // act
            ValidationResult result = await _validator.ValidationAsync( createEventCommand );

            // assert
            Assert.IsTrue( result.IsFail );
        }

        [Test]
        public async Task ValidationAsync_EndDateIncludedInExistDateEvent_Fail()
        {
            // arrange
            CreateEventCommand createEventCommand = new CreateEventCommand
            {
                UserId = 1,
                Name = "name",
                Description = "",
                StartEvent = new DateTime( 500 ),
                EndEvent = new DateTime( 1500 )
            };

            // act
            ValidationResult result = await _validator.ValidationAsync( createEventCommand );

            // assert
            Assert.IsTrue( result.IsFail );
        }

        [Test]
        public async Task ValidationAsyncInputDatewEventIncludedExistDateEvent_Fail()
        {
            // arrange
            CreateEventCommand createEventCommand = new CreateEventCommand
            {
                UserId = 1,
                Name = "name",
                Description = "",
                StartEvent = new DateTime( 100 ),
                EndEvent = new DateTime( 2500 )
            };

            // act
            ValidationResult result = await _validator.ValidationAsync( createEventCommand );

            // assert
            Assert.IsTrue( result.IsFail );
        }

        [Test]
        public async Task ValidationAsync_ExistDateEventIncludedInputDateEvent_Fail()
        {
            // arrange
            CreateEventCommand createEventCommand = new CreateEventCommand
            {
                UserId = 1,
                Name = "name",
                Description = "",
                StartEvent = new DateTime( 1100 ),
                EndEvent = new DateTime( 1500 )
            };

            // act
            ValidationResult result = await _validator.ValidationAsync( createEventCommand );

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
